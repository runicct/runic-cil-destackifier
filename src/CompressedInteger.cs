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
using System.Collections.Generic;

namespace Runic.CIL
{
    public abstract partial class Destackifier
    {
        internal static uint ReadCompressedInteger(byte[] data, ref uint offset)
        {
            byte firstByte = data[offset]; offset++;
            if ((firstByte & 0x80) == 0) { return (uint)firstByte; }
            byte secondByte = data[offset]; offset++;
            if ((firstByte & 0x40) == 0) { return (uint)(((uint)firstByte << 8) | (uint)secondByte) & 0x3FFF; }
            byte thirdByte = data[offset]; offset++;
            byte forthByte = data[offset]; offset++;

            return (uint)(((uint)firstByte << 24) | ((uint)secondByte << 16) | ((uint)thirdByte << 8) | ((uint)forthByte));
        }
    }
}
