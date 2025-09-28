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
            Destackifier _parent;
            Dictionary<uint, Signature> _methodSignatures = new Dictionary<uint, Signature>();
            Dictionary<int, Information> _information = new Dictionary<int, Information>();
            int _maxStackSize = 0;
            public int MaxStackSize { get { return _maxStackSize; } }
#if NET5_0_OR_GREATER
            public Information? this[int offset]
#else
            public Information this[int offset]
#endif
            {
                get
                {
                    Information info;
                    if (_information.TryGetValue(offset, out info)) { return info; }
                    return null;
                }
            }
            public class Information
            {
                internal List<Information> BranchTo = new List<Information>();
                internal bool IsFallThrough = false;
                internal bool IsTarget = false;
                internal int Offset;
                internal int SaveStackSize = -1;
                internal int RestoreStackSize = -1;
                public Information(int offset)
                {
                    Offset = offset;
                }
            }
            internal Information GetOrCreateInformation(int offset)
            {
                Information info;
                if (!_information.TryGetValue(offset, out info))
                {
                    info = new Information(offset);
                    info.Offset = offset;
                    _information.Add(offset, info);
                }
                return info;
            }
            public BranchInformation(Dictionary<uint, Signature> signatures)
            {
                _methodSignatures = signatures;
            }
#if NET6_0_OR_GREATER
            public void Process(Span<byte> bytecode)
            {
                BranchLocationDisassembler branchLocationDisassembler = new BranchLocationDisassembler(this);
                branchLocationDisassembler.Process(bytecode);
                FallThroughDetector fallThroughDetector = new FallThroughDetector(this);
                fallThroughDetector.Process(bytecode);
                StackSizeDisassembler stackSizeDisassembler = new StackSizeDisassembler(this);
                stackSizeDisassembler.Process(bytecode);
                _maxStackSize = stackSizeDisassembler.MaxStackSize;
            }
#endif
            public void Process(byte[] bytecode)
            {
                BranchLocationDisassembler branchLocationDisassembler = new BranchLocationDisassembler(this);
                branchLocationDisassembler.Process(bytecode);
                FallThroughDetector fallThroughDetector = new FallThroughDetector(this);
                fallThroughDetector.Process(bytecode);
                StackSizeDisassembler stackSizeDisassembler = new StackSizeDisassembler(this);
                stackSizeDisassembler.Process(bytecode);
                _maxStackSize = stackSizeDisassembler.MaxStackSize;
            }
            class FallThroughDisassembler : Disassembler
            {
              
            }
        }
    }
}