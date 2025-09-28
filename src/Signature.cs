/*
 * MIT License
 * 
 * Copyright (c) 2025 Runic Compiler Toolkit Contributors
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;

namespace Runic.CIL
{
    public abstract partial class Destackifier
    {
        internal class Signature
        {
            bool IsVoidType(byte[] signature, ref uint index)
            {
                switch (signature[index])
                {
                    case 0x01: return true;
                    case 0x1F:
                        {
                            index++;
                            ReadCompressedInteger(signature, ref index);
                            return IsVoidType(signature, ref index);
                        }
                    default:
                        return false;
                }
            }
            uint _parametersCount;
            public uint ParametersCount { get { return _parametersCount; } }
            bool _returnVoid;
            public bool ReturnVoid { get { return _returnVoid; } }
            bool _hasThis = false;
            public bool HasThis { get { return _hasThis; } }
            bool _explicitThis = false;
            public Signature(Destackifier parent, byte[] signature)
            {
                if (signature == null || signature.Length == 0)
                {
                    _returnVoid = true;
                    _parametersCount = 0;
                    return;
                }

                uint paramSignatureByteIndex = 0;

                byte flag = signature[paramSignatureByteIndex];
                paramSignatureByteIndex++;
                if ((flag & 0x20) != 0) { _hasThis = true; }
                if ((flag & 0x40) != 0) { _explicitThis = true; }
                if ((flag & 0x10) != 0)
                {
                    // Generic Parameters count
                   ReadCompressedInteger(signature, ref paramSignatureByteIndex);
                }

                _parametersCount = ReadCompressedInteger(signature, ref paramSignatureByteIndex);
                _returnVoid = IsVoidType(signature, ref paramSignatureByteIndex);
            }
        }
    }
}
