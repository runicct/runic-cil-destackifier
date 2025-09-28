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
        partial class BranchInformation
        {
            internal class FallThroughDetector : Disassembler
            {
                BranchInformation _parent;
                public FallThroughDetector(BranchInformation parent)
                {
                    _parent = parent;
                }
#if NET6_0_OR_GREATER
                public void Process(Span<byte> bytecode)
                {
                    Disassemble(bytecode, 0, bytecode.Length);
                }

#endif
                public void Process(byte[] bytecode)
                {
                    Disassemble(bytecode, 0, bytecode.Length);
                }

#if NET5_0_OR_GREATER
                Information? GetInformation(int address)
#else
                Information GetInformation(int address)
#endif
                {
                    Information info;
                    if (!_parent._information.TryGetValue(address, out info)) { return null; }
                    return info;
                }
                bool _fallthrough = false;
                public override void Fetch(int offset)
                {
#if NET5_0_OR_GREATER
                    Information? info = GetInformation(offset);
#else
                    Information info = GetInformation(offset);
#endif
                    if (info != null) { info.IsFallThrough = _fallthrough; }
                    _fallthrough = true;
                }
                public override void Br(int offset, int address) { _fallthrough = false; }
                public override void BrFalse(int offset, int address) { _fallthrough = false; }
                public override void BrTrue(int offset, int address) { _fallthrough = false; }
                public override void BrEq(int offset, int address) { _fallthrough = false; }
                public override void BrGt(int offset, int address) { _fallthrough = false; }
                public override void BrGe(int offset, int address) { _fallthrough = false; }
                public override void BrLt(int offset, int address) { _fallthrough = false; }
                public override void BrLe(int offset, int address) { _fallthrough = false; }
                public override void BrNeqUn(int offset, int address) { _fallthrough = false; }
                public override void BrGeUn(int offset, int address) { _fallthrough = false; }
                public override void BrGtUn(int offset, int address) { _fallthrough = false; }
                public override void BrLtUn(int offset, int address) { _fallthrough = false; }
                public override void BrLeUn(int offset, int address) { _fallthrough = false; }
                public override void Switch(int offset, int[] address) { _fallthrough = false; }
                public override void Leave(int offset, int address) { _fallthrough = false; }
                public override void Rethrow(int offset) { _fallthrough = false; }
                public override void Throw(int offset) { _fallthrough = false; }
                public override void Ret(int offset) { _fallthrough = false; }
                public override void Jmp(int offset, uint methodToken) { _fallthrough = false; }
            }
        }
    }
}
