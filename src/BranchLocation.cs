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
        partial class BranchInformation
        {
            class BranchLocationDisassembler : Disassembler
            {
                BranchInformation _parent;
                public BranchLocationDisassembler(BranchInformation parent)
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
                Information GetInformation(int address)
                {
                    Information info;
                    if (!_parent._information.TryGetValue(address, out info))
                    {
                        info = new Information(address);
                        _parent._information.Add(address, info);
                    }
                    return info;
                }

                public override void Br(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;

                }
                public override void BrFalse(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrTrue(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrEq(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrNeqUn(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrGe(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }

                public override void BrGt(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrLe(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }

                public override void BrLt(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrGeUn(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrGtUn(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrLeUn(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void BrLtUn(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
                public override void Switch(int offset, int[] address)
                {
                    Information current = GetInformation(offset);

                    for (int n = 0; n < address.Length; n++)
                    {
                        Information destination = GetInformation(address[n]);
                        destination.IsTarget = true;
                    }
                }
                public override void Leave(int offset, int address)
                {
                    Information destination = GetInformation(address);
                    Information current = GetInformation(offset);
                    current.BranchTo.Add(destination);
                    destination.IsTarget = true;
                }
            }
        }
    }
}