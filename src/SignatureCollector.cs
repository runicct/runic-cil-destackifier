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
        public abstract byte[] GetMethodSignature(uint methodToken);
        class SignatureCollector : Disassembler
        {
            Destackifier _parent;
            Dictionary<uint, Signature> _methodSignatures = new Dictionary<uint, Signature>();
            public SignatureCollector(Destackifier parent)
            {
                _parent = parent;
            }
#if NET6_0_OR_GREATER
            public Dictionary<uint, Signature> Process(Span<byte> bytecode)
            {
                Disassemble(bytecode, 0, bytecode.Length);
                return _methodSignatures;
            }
#endif
            public Dictionary<uint, Signature> Process(byte[] bytecode)
            {
                Disassemble(bytecode, 0, bytecode.Length);
                return _methodSignatures;
            }
            public override void NewObj(int offset, uint ctorToken)
            {
                if (!_methodSignatures.ContainsKey(ctorToken))
                {
                    byte[] signature = _parent.GetMethodSignature(ctorToken);
                    _methodSignatures.Add(ctorToken, new Signature(_parent, signature));
                }
            }
            public override void Call(int offset, bool tail, uint methodToken)
            {
                if (!_methodSignatures.ContainsKey(methodToken))
                {
                    byte[] signature = _parent.GetMethodSignature(methodToken);
                    _methodSignatures.Add(methodToken, new Signature(_parent, signature));
                }
            }
            public override void CallVirt(int offset, bool noNullCheck, uint constrainedType, bool tail, uint methodToken)
            {
                if (!_methodSignatures.ContainsKey(methodToken))
                {
                    byte[] signature = _parent.GetMethodSignature(methodToken);
                    _methodSignatures.Add(methodToken, new Signature(_parent, signature));
                }
            }
            public override void Calli(int offset, bool tail, uint descriptorToken)
            {
                if (!_methodSignatures.ContainsKey(descriptorToken))
                {
                    byte[] signature = _parent.GetMethodSignature(descriptorToken);
                    _methodSignatures.Add(descriptorToken, new Signature(_parent, signature));
                }
            }
        }
    }
}
