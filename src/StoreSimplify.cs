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
        class StoreSimplify : Disassembler
        {
            public class StoreInfo
            {
                int _lastInstructionOffset;
                public int LastInstructionOffset { get { return _lastInstructionOffset; } }
                int _storeOffset;
                public int StoreOffset { get { return _storeOffset; } }
                int _index;
                public int Index { get { return _index; } }
                public StoreInfo(int lastInstructionOffset, int storeOffset, int index)
                {
                    _lastInstructionOffset = lastInstructionOffset;
                    _storeOffset = storeOffset;
                    _index = index;
                }
            }
            BranchInformation _branchInformation;
            Dictionary<int, StoreInfo> _storesByLastInstructionOffset = new Dictionary<int, StoreInfo>();
            Dictionary<int, StoreInfo> _storesByStoreOffset = new Dictionary<int, StoreInfo>();
            StoreSimplifyDisassembler _disassembler;
            public StoreSimplify(BranchInformation branchInformation)
            {
                _branchInformation = branchInformation;
                _disassembler = new StoreSimplifyDisassembler(this);
            }
#if NET6_0_OR_GREATER
            public StoreInfo? GetStoreInfo(int offset)
#else
            public StoreInfo GetStoreInfo(int offset)
#endif
            {
                StoreInfo storeInfo;
                if (!_storesByLastInstructionOffset.TryGetValue(offset, out storeInfo)) { return null; }
                return storeInfo;
            }
#if NET6_0_OR_GREATER
            public StoreInfo? GetStoreInfoByStoreOffset(int offset)
#else
            public StoreInfo GetStoreInfoByStoreOffset(int offset)
#endif
            {
                StoreInfo storeInfo;
                if (!_storesByStoreOffset.TryGetValue(offset, out storeInfo)) { return null; }
                return storeInfo;
            }

#if NET6_0_OR_GREATER
            public void Process(Span<byte> bytecode)
            {
                _disassembler.Disassemble(bytecode, 0, bytecode.Length);
            }

#endif
            public void Process(byte[] bytecode)
            {
                _disassembler.Disassemble(bytecode, 0, bytecode.Length);
            }
            class StoreSimplifyDisassembler : Disassembler
            {
                StoreSimplify _parent;
                public StoreSimplifyDisassembler(StoreSimplify parent)
                {
                    _parent = parent;
                }

                int _offset = -1;
                int _previousOffset = -1;
                public override void Fetch(int offset)
                {
                    _previousOffset = _offset;
                    _offset = offset;
#if NET6_0_OR_GREATER
                    BranchInformation.Information? info = _parent._branchInformation[offset];
#else
                    BranchInformation.Information info = _parent._branchInformation[offset];
#endif
                    if (info != null) { _previousOffset = -1; return; }
                }
                public override void Nop(int offset) { _offset = _previousOffset; }
                public override void Jmp(int offset, uint methodToken)
                {
                    _offset = -1;
                    _previousOffset = -1;
                }
                public override void EndFilter(int offset)
                {
                    _offset = -1;
                    _previousOffset = -1;
                }
                public override void EndFinally(int offset)
                {
                    _offset = -1;
                    _previousOffset = -1;
                }
                public override void Break(int offset)
                {
                    _offset = -1;
                    _previousOffset = -1;
                }
                public override void StLoc(int offset, int index)
                {

                    if ((_offset != -1) && (_previousOffset != -1))
                    {
                        StoreInfo storeInfo = new StoreInfo(_previousOffset, offset, index);
                        _parent._storesByLastInstructionOffset.Add(_previousOffset, storeInfo);
                        _parent._storesByStoreOffset.Add(offset, storeInfo);
                    }
                    _offset = -1;
                    _previousOffset = -1;
                }
            }
        }



    }
}
