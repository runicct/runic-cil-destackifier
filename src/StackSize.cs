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
            class StackSizeDisassembler : Disassembler
            {
                BranchInformation _parent;
                HashSet<int> _chunk = new HashSet<int>();
                int _stackSize = 0;
                int _maxStackSize = 0;
                public int MaxStackSize { get { return _maxStackSize; } }
                void UpdateMaxStack() { if (_stackSize > _maxStackSize) { _maxStackSize = _stackSize; } }
                public StackSizeDisassembler(BranchInformation parent)
                {
                    _parent = parent;
                    foreach (Information information in parent._information.Values)
                    {
                        if (information.IsTarget)
                        {
                            _chunk.Add(information.Offset);
                        }
                    }
                }

#if NET6_0_OR_GREATER
                public void Process(Span<byte> bytecode)
                {
                    Disassemble(bytecode, 0, bytecode.Length);
                    bool solvedOne = false;
                    HashSet<int> chunks = _chunk;
                    HashSet<int> nextChunks;
                    do
                    {
                        solvedOne = false;
                        nextChunks = new HashSet<int>();
                        foreach (int chunk in chunks)
                        {
                            Information current = GetInformation(chunk);
                            if (current.RestoreStackSize >= 0)
                            {
                                _stackSize = current.RestoreStackSize;
                                Disassemble(bytecode, chunk, bytecode.Length);
                                solvedOne = true;
                            }
                            else
                            {
                                nextChunks.Add(chunk);
                            }
                        }
                        chunks = nextChunks;
                    } while (solvedOne);

                    foreach (int chunk in _chunk)
                    {
                        Information current = GetInformation(chunk);
                        if (current.RestoreStackSize < 0)
                        {
                            current.RestoreStackSize = 0;
                            _stackSize = current.RestoreStackSize;
                            Disassemble(bytecode, chunk, bytecode.Length);
                        }
                    }
                }
#endif

                public void Process(byte[] bytecode)
                {
                    Disassemble(bytecode, 0, bytecode.Length);
                    bool solvedOne = false;
                    HashSet<int> chunks = _chunk;
                    HashSet<int> nextChunks;
                    do
                    {
                        solvedOne = false;
                        nextChunks = new HashSet<int>();
                        foreach (int chunk in chunks)
                        {
                            Information current = GetInformation(chunk);
                            if (current.RestoreStackSize >= 0)
                            {
                                _stackSize = current.RestoreStackSize;
                                Disassemble(bytecode, chunk, bytecode.Length);
                                solvedOne = true;
                            }
                            else
                            {
                                nextChunks.Add(chunk);
                            }
                        }
                        chunks = nextChunks;
                    } while (solvedOne);

                    foreach (int chunk in _chunk)
                    {
                        Information current = GetInformation(chunk);
                        if (current.RestoreStackSize < 0)
                        {
                            current.RestoreStackSize = 0;
                            _stackSize = current.RestoreStackSize;
                            Disassemble(bytecode, chunk, bytecode.Length);
                        }
                    }
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
                public override void BrFalse(int offset, int address)
                {
                    _stackSize -= 1;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrTrue(int offset, int address)
                {
                    _stackSize -= 1;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrEq(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrNeqUn(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrGt(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrGe(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrLt(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrLe(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrGeUn(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrGtUn(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrLeUn(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void BrLtUn(int offset, int address)
                {
                    _stackSize -= 2;
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                }
                public override void Br(int offset, int address)
                {
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                    Abort();
                }
                public override void Switch(int offset, int[] address)
                {
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
#else
                    Information current = GetInformation(offset);
#endif
                    if (current.SaveStackSize < 0) { current.SaveStackSize = _stackSize; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    for (int n = 0; n < address.Length; n++)
                    {
#if NET5_0_OR_GREATER
                        Information? target = GetInformation(address[n]);
#else
                        Information target = GetInformation(address[n]);
#endif
                        if (target.RestoreStackSize < 0) { target.RestoreStackSize = _stackSize; }
                        else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }

                    }
                }
                public override void Leave(int offset, int address)
                {
#if NET5_0_OR_GREATER
                    Information? current = GetInformation(offset);
                    Information? target = GetInformation(address);
#else
                    Information current = GetInformation(offset);
                    Information target = GetInformation(address);
#endif
                    if (_stackSize != 0) { throw new Exception("Stack was not empty before a leave instruction IL:" + offset.ToString()); }
                    if (current.SaveStackSize < 0) { current.SaveStackSize = 0; }
                    else if (current.SaveStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + offset.ToString()); }
                    if (target.RestoreStackSize < 0) { target.RestoreStackSize = 0; }
                    else if (target.RestoreStackSize != _stackSize) { throw new Exception("Invalid Stack size IL:" + address.ToString()); }
                    Abort();
                }
                public override void Throw(int offset) { Abort(); }
                public override void Rethrow(int offset) { Abort(); }
                public override void Ret(int offset) { Abort(); }
                public override void StIndI1(int offset, bool volatilePrefix) { _stackSize -= 2; }
                public override void StIndI2(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void StIndI4(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void StIndI8(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void StIndR4(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void StIndR8(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void StIndI(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void LdcI4(int offset, int constant) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdcI8(int offset, long constant) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdcR4(int offset, float constant) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdcR8(int offset, double constant) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdArg(int offset, int index) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdArgA(int offset, int index) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdSFld(int offset, bool volatilePrefix, uint fieldToken) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdSFldA(int offset, uint fieldToken) { _stackSize += 1; UpdateMaxStack(); }
                public override void StSFld(int offset, bool volatilePrefix, uint fieldToken) { _stackSize -= 1; }
                public override void StArg(int offset, int index) { _stackSize -= 1; }
                public override void LdLoc(int offset, int index) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdLocA(int offset, int index) { _stackSize += 1; UpdateMaxStack(); }
                public override void StLoc(int offset, int index) { _stackSize -= 1; }
                public override void NewObj(int offset, uint ctorToken)
                {
                    _stackSize -= (int)_parent._methodSignatures[ctorToken].ParametersCount;
                    _stackSize += 1;
                }
                public override void LdElemI1(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemI2(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemI4(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemI8(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemU1(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemU2(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemU4(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemR4(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemR8(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElem(int offset, bool noNullCheck, bool noBoundCheck, uint typeToken) { _stackSize -= 1; }
                public override void LdElemA(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, bool readOnly, uint typeToken) { _stackSize -= 1; }
                public override void LdElemI(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void LdElemRef(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 1; }
                public override void StElemI1(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemI2(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemI4(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemI8(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemR4(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemR8(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElemRef(int offset, bool noNullCheck, bool noBoundCheck) { _stackSize -= 3; }
                public override void StElem(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, uint typeToken) { _stackSize -= 3; }
                public override void LdNull(int offset) { _stackSize += 1; UpdateMaxStack(); }
                public override void Dup(int offset) { _stackSize += 1; UpdateMaxStack(); }
                public override void Pop(int offset) { _stackSize -= 1; }
                public override void Add(int offset) { _stackSize -= 1; }
                public override void AddOvf(int offset) { _stackSize -= 1; }
                public override void AddOvfUn(int offset) { _stackSize -= 1; }
                public override void Mul(int offset) { _stackSize -= 1; }
                public override void MulOvf(int offset) { _stackSize -= 1; }
                public override void MulOvfUn(int offset) { _stackSize -= 1; }
                public override void Sub(int offset) { _stackSize -= 1; }
                public override void SubOvf(int offset) { _stackSize -= 1; }
                public override void SubOvfUn(int offset) { _stackSize -= 1; }
                public override void Div(int offset) { _stackSize -= 1; }
                public override void DivUn(int offset) { _stackSize -= 1; }
                public override void Rem(int offset) { _stackSize -= 1; }
                public override void RemUn(int offset) { _stackSize -= 1; }
                public override void Shl(int offset) { _stackSize -= 1; }
                public override void ShrUn(int offset) { _stackSize -= 1; }
                public override void Shr(int offset) { _stackSize -= 1; }
                public override void And(int offset) { _stackSize -= 1; }
                public override void Or(int offset) { _stackSize -= 1; }
                public override void Xor(int offset) { _stackSize -= 1; }
                public override void CltUn(int offset) { _stackSize -= 1; }
                public override void Clt(int offset) { _stackSize -= 1; }
                public override void CgtUn(int offset) { _stackSize -= 1; }
                public override void Cgt(int offset) { _stackSize -= 1; }
                public override void Ceq(int offset) { _stackSize -= 1; }
                public override void LdStr(int offset, uint literalStringToken) { _stackSize += 1; UpdateMaxStack(); }
                public override void LdFtn(int offset, uint methodToken) { _stackSize += 1; UpdateMaxStack(); }
                public override void SizeOf(int offset, uint typeToken) { _stackSize += 1; UpdateMaxStack(); }
                public override void StObj(int offset, bool volatilePrefix, int alignment, uint typeToken) { _stackSize -= 2; }
                public override void StFld(int offset, bool noTypeCheck, bool volatilePrefix, int alignment, uint fieldToken) { _stackSize -= 2; }
                public override void StIndRef(int offset, bool volatilePrefix, int alignment) { _stackSize -= 2; }
                public override void CpBlk(int offset, bool volatilePrefix, int alignment) { _stackSize -= 3; }
                public override void CopyObj(int offset, uint typeToken) { _stackSize -= 2; }
                public override void InitBlk(int offset, bool volatilePrefix, int alignment) { _stackSize -= 3; }
                public override void LdObj(int offset, bool volatilePrefix, int alignment, uint typeToken) { }
                public override void InitObj(int offset, uint typeToken) { _stackSize -= 1; }
                public override void LdToken(int offset, uint token) { _stackSize += 1; UpdateMaxStack(); }
                public override void Nop(int offset) { }
                public override void LdFld(int offset, bool noTypeCheck, bool volatilePrefix, int alignment, uint fieldToken) { }
                public override void LdFldA(int offset, uint fieldToken) { }
                public override void LdLen(int offset) { }
                public override void LocAlloc(int offset) { }
                public override void NewArr(int offset, uint typeToken) { }
                public override void Unbox(int offset, bool noTypeCheck, uint typeToken) { }
                public override void UnboxAny(int offset, uint typeToken) { }
                public override void LdIndI(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndI1(int offset, bool volatilePrefix) { }
                public override void LdIndI2(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndI4(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndI8(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndR4(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndR8(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndU1(int offset, bool volatilePrefix) { }
                public override void LdIndU2(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndU4(int offset, bool volatilePrefix, int alignment) { }
                public override void LdIndRef(int offset, bool volatilePrefix, int alignment) { }
                public override void ConvI(int offset) { }
                public override void ConvI1(int offset) { }
                public override void ConvI2(int offset) { }
                public override void ConvI4(int offset) { }
                public override void ConvI8(int offset) { }
                public override void ConvOvfI(int offset) { }
                public override void ConvOvfU(int offset) { }
                public override void ConvR4(int offset) { }
                public override void ConvR8(int offset) { }
                public override void EndFilter(int offset) { _stackSize -= 1; }
                public override void Call(int offset, bool tail, uint methodToken)
                {
                    Signature signature = _parent._methodSignatures[methodToken];
                    _stackSize -= (int)signature.ParametersCount;
                    if (signature.HasThis) { _stackSize -= 1; }
                    if (!signature.ReturnVoid) { _stackSize += 1; }
                    UpdateMaxStack();
                }
                public override void CallVirt(int offset, bool noNullCheck, uint constrainedType, bool tail, uint methodToken)
                {
                    Signature signature = _parent._methodSignatures[methodToken];
                    _stackSize -= (int)signature.ParametersCount;
                    if (signature.HasThis) { _stackSize -= 1; }
                    if (!signature.ReturnVoid) { _stackSize += 1; }
                    UpdateMaxStack();
                }
                public override void Calli(int offset, bool tail, uint descriptorToken)
                {
                    Signature signature = _parent._methodSignatures[descriptorToken];
                    _stackSize -= (int)signature.ParametersCount;
                    if (signature.HasThis) { _stackSize -= 1; }
                    if (!signature.ReturnVoid) { _stackSize += 1; }
                    UpdateMaxStack();
                }
                public override void ArgList(int offset) { _stackSize += 1; UpdateMaxStack(); }
                public override void Jmp(int offset, uint methodToken)
                {
                    if (_stackSize != 0) { throw new Exception("Stack was not empty before a jmp instruction IL:" + offset.ToString()); }
                    Abort();
                }
            }
        }
    }
}
