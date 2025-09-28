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
        class DestackifyDisassembler : Disassembler
        {
            public class SaveSlot
            {
                internal int Offset;
                internal int StackSize;
                internal int[] Locals;
                public SaveSlot(int offset, int stackSize)
                {
                    Offset = offset;
                    StackSize = stackSize;
                    Locals = new int[stackSize];
                }
            }

            Destackifier _parent;
            int[] _stack;
            Signature _signature;
            BranchInformation _branchInformation;
            Dictionary<uint, Signature> _signatures;
            HashSet<int> _locals = new HashSet<int>();
            internal DestackifyDisassembler(BranchInformation branchInformation, Dictionary<uint, Signature> signatures, HashSet<int> locals, uint maxStackSize, Signature signature, Destackifier parent)
            {
                _locals = locals;
                _parent = parent;
                _stack = new int[maxStackSize];
                _branchInformation = branchInformation;
                _signature = signature;
                _signatures = signatures;
            }
#if NET6_0_OR_GREATER
            public void Process(Span<byte> il)
            {
                Disassemble(il, 0, il.Length);
            }
#endif
            public void Process(byte[] il)
            {
                Disassemble(il, 0, il.Length);
            }
            int _nextLocal = 0;
            public int DeclareLocal()
            {
                while (_locals.Contains(_nextLocal)) { _nextLocal++; }
                return _nextLocal++;
            }

            Dictionary<int, SaveSlot> _saveSlots = new Dictionary<int, SaveSlot>();
            public void PopulateSaveSlot(int offset)
            {
                BranchInformation.Information info = _branchInformation[offset];

                foreach (BranchInformation.Information branchTo in info.BranchTo)
                {
                    SaveSlot saveSlot;
                    if (_saveSlots.TryGetValue(branchTo.Offset, out saveSlot))
                    {
                        for (int n = 0; n < saveSlot.StackSize; n++)
                        {
                            int local = Pop();
                            _parent.StLoc(offset, saveSlot.Locals[n], local);
                        }
                        return;
                    }
                    saveSlot = new SaveSlot(branchTo.Offset, info.SaveStackSize);
                    _saveSlots.Add(branchTo.Offset, saveSlot);

                    for (int n = 0; n < info.SaveStackSize; n++)
                    {
                        int local = Pop();
                        saveSlot.Locals[n] = DeclareLocal();
                        _parent.StLoc(offset, saveSlot.Locals[n], local);
                    }
                }
            }

            public int Pop()
            {
                int id = _stack[0];
                for (int n = 0; n < _stack.Length - 1; n++)
                {
                    _stack[n] = _stack[n + 1];
                }
                return id;
            }

            public void Push(int id)
            {
                for (int n = _stack.Length - 1; n > 0; n--)
                {
                    _stack[n] = _stack[n - 1];
                }
                _stack[0] = id;
            }
            int previousOffset = 0;
            public override void Fetch(int offset)
            {
#if NET5_0_OR_GREATER
                BranchInformation.Information? info = _branchInformation[offset];
#else
                BranchInformation.Information info = _branchInformation[offset];
#endif
                if (info == null)
                {
                    previousOffset = offset;
                    return;
                }

                if (info.RestoreStackSize > 0)
                {
                    SaveSlot saveSlot;
                    if (_saveSlots.TryGetValue(offset, out saveSlot))
                    {
                        if (info.IsFallThrough)
                        {
                            for (int n = 0; n < info.RestoreStackSize; n++)
                            {
                                _parent.StLoc(previousOffset, saveSlot.Locals[n], Pop());
                            }
                        }
                        for (int n = 0; n < saveSlot.StackSize; n++)
                        {
                            Push(saveSlot.Locals[n]);
                        }
                        previousOffset = offset;
                        return;
                    }

                    saveSlot = new SaveSlot(offset, info.RestoreStackSize);
                    _saveSlots.Add(offset, saveSlot);

                    for (int n = 0; n < info.RestoreStackSize; n++)
                    {
                        saveSlot.Locals[n] = DeclareLocal();
                    }
                    if (info.IsFallThrough)
                    {
                        for (int n = 0; n < info.RestoreStackSize; n++)
                        {
                            _parent.StLoc(offset, saveSlot.Locals[n], Pop());
                        }
                    }
                    for (int n = 0; n < info.RestoreStackSize; n++)
                    {
                        Push(saveSlot.Locals[n]);
                    }
                }
                previousOffset = offset;
            }
            public override void Nop(int offset) { _parent.Nop(offset); }
            public override void Add(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Add(offset, dest, a, b);
                Push(dest);
            }
            public override void AddOvf(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.AddOvf(offset, dest, a, b);
                Push(dest);
            }
            public override void AddOvfUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.AddOvfUn(offset, dest, a, b);
                Push(dest);
            }
            public override void And(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.And(offset, dest, a, b);
                Push(dest);
            }
            public override void Or(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Or(offset, dest, a, b);
                Push(dest);
            }
            public override void Xor(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Xor(offset, dest, a, b);
                Push(dest);
            }
            public override void Sub(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Sub(offset, dest, a, b);
                Push(dest);
            }
            public override void SubOvf(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.SubOvf(offset, dest, a, b);
                Push(dest);
            }
            public override void SubOvfUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.SubOvfUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Mul(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Mul(offset, dest, a, b);
                Push(dest);
            }
            public override void MulOvf(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.MulOvf(offset, dest, a, b);
                Push(dest);
            }
            public override void MulOvfUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.MulOvfUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Div(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Div(offset, dest, a, b);
                Push(dest);
            }
            public override void DivUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.DivUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Rem(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Rem(offset, dest, a, b);
                Push(dest);
            }
            public override void RemUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.RemUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Shr(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Shr(offset, dest, a, b);
                Push(dest);
            }
            public override void ShrUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ShrUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Shl(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Shl(offset, dest, a, b);
                Push(dest);
            }
            public override void Not(int offset)
            {
                int value = Pop();
                int dest = DeclareLocal();
                _parent.Not(offset, dest, value);
                Push(dest);
            }
            public override void SizeOf(int offset, uint typeToken)
            {
                int dest = DeclareLocal();
                _parent.SizeOf(offset, typeToken, dest);
                Push(dest);
            }
            public override void LdObj(int offset, bool volatilePrefix, int alignment, uint typeToken)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdObj(offset, volatilePrefix, alignment, typeToken, dest, address);
                Push(dest);
            }
            public override void StObj(int offset, bool volatilePrefix, int alignment, uint typeToken)
            {
                int value = Pop();
                int address = Pop();
                _parent.StObj(offset, volatilePrefix, alignment, typeToken, address, value);
            }

            public override void LdToken(int offset, uint token)
            {
                int dest = DeclareLocal();
                _parent.LdToken(offset, token, dest);
                Push(dest);
            }
            public override void Neg(int offset)
            {
                int src = Pop();
                int dest = DeclareLocal();
                _parent.Neg(offset, dest, src);
                Push(dest);
            }
            public override void LocAlloc(int offset)
            {
                int size = Pop();
                int dest = DeclareLocal();
                _parent.LocAlloc(offset, dest, size);
                Push(dest);
            }
            public override void StElemI(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemI(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemRef(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemRef(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void EndFinally(int offset) { _parent.EndFinally(offset); }
            public override void LdArg(int offset, int index)
            {
                int dest = DeclareLocal();
                _parent.LdArg(offset, dest, index);
                Push(dest);
            }
            public override void StArg(int offset, int index)
            {
                int src = Pop();
                _parent.StArg(offset, index, src);
            }
            public override void LdLocA(int offset, int index)
            {
                int dest = DeclareLocal();
                _parent.LdLocA(offset, dest, index);
                Push(dest);
            }
            public override void LdLoc(int offset, int index)
            {
                if (_branchInformation[offset] != null) { _parent.Nop(offset); }
                Push(index);
            }
            public override void StLoc(int offset, int index)
            {
                int src = Pop();
                _parent.StLoc(offset, index, src);
            }
            public override void Pop(int offset)
            {
                if (_branchInformation[offset] != null) { _parent.Nop(offset); }
                Pop();
            }
            public override void Dup(int offset)
            {
                if (_branchInformation[offset] != null) { _parent.Nop(offset); }
                int a = Pop();
                Push(a);
                Push(a);
            }
            public override void LdLen(int offset)
            {
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdLen(offset, dest, array);
                Push(dest);
            }
            public override void Ceq(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Ceq(offset, dest, a, b);
                Push(dest);
            }
            public override void Clt(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Clt(offset, dest, a, b);
                Push(dest);
            }
            public override void CltUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.CltUn(offset, dest, a, b);
                Push(dest);
            }
            public override void Cgt(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.Cgt(offset, dest, a, b);
                Push(dest);
            }
            public override void CgtUn(int offset)
            {
                int b = Pop();
                int a = Pop();
                int dest = DeclareLocal();
                _parent.CgtUn(offset, dest, a, b);
                Push(dest);
            }
            public override void ConvU(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvU(offset, dest, a);
                Push(dest);
            }

            public override void ConvI(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvI(offset, dest, a);
                Push(dest);
            }

            public override void ConvI1(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvI1(offset, dest, a);
                Push(dest);
            }
            public override void ConvI2(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvI2(offset, dest, a);
                Push(dest);
            }
            public override void ConvI4(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvI4(offset, dest, a);
                Push(dest);
            }
            public override void ConvI8(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvI8(offset, dest, a);
                Push(dest);
            }
            public override void ConvU1(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvU1(offset, dest, a);
                Push(dest);
            }
            public override void ConvU2(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvU2(offset, dest, a);
                Push(dest);
            }
            public override void ConvU4(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvU4(offset, dest, a);
                Push(dest);
            }
            public override void ConvU8(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvU8(offset, dest, a);
                Push(dest);
            }
            public override void ConvR4(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvR4(offset, dest, a);
                Push(dest);
            }
            public override void ConvR8(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvR8(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI1(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI1(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI2(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI2(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI4(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI4(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI8(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI8(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU1(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU1(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU2(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU2(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU4(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU4(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU8(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU8(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfIUn(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfIUn(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI1Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI1Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI2Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI2Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI4Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI4Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfI8Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfI8Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfUUn(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfUUn(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU1Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU1Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU2Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU2Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU4Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU4Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvOvfU8Un(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvOvfU8Un(offset, dest, a);
                Push(dest);
            }
            public override void ConvRUn(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.ConvRUn(offset, dest, a);
                Push(dest);
            }
            public override void Call(int offset, bool tail, uint methodToken)
            {
                Signature methodSignature = _signatures[methodToken];
                uint parameterCount = methodSignature.ParametersCount;
                if (methodSignature.HasThis) { parameterCount += 1; }
                int[] parameters = new int[parameterCount];
                for (int n = parameters.Length - 1; n >= 0; n--) { parameters[n] = Pop(); }

                if (!methodSignature.ReturnVoid)
                {
                    int dest = DeclareLocal();
                    _parent.Call(offset, tail, methodToken, dest, parameters);
                    Push(dest);
                }
                else
                {
                    _parent.Call(offset, tail, methodToken, parameters);
                }
            }
            public override void CallVirt(int offset, bool noNullCheck, uint constrainedType, bool tail, uint methodToken)
            {
                Signature methodSignature = _signatures[methodToken];
                uint parameterCount = methodSignature.ParametersCount;
                if (methodSignature.HasThis) { parameterCount += 1; }
                int[] parameters = new int[parameterCount];
                for (int n = parameters.Length - 1; n >= 0; n--) { parameters[n] = Pop(); }

                if (!methodSignature.ReturnVoid)
                {
                    int dest = DeclareLocal();
                    _parent.CallVirt(offset, noNullCheck, tail, methodToken, dest, parameters);
                    Push(dest);
                }
                else
                {
                    _parent.CallVirt(offset, noNullCheck, tail, methodToken, parameters);
                }
            }
            public override void Calli(int offset, bool tail, uint descriptorToken)
            {
                Signature methodSignature = _signatures[descriptorToken];
                uint parameterCount = methodSignature.ParametersCount;
                if (methodSignature.HasThis) { parameterCount += 1; }
                int[] parameters = new int[parameterCount];
                for (int n = parameters.Length - 1; n >= 0; n--) { parameters[n] = Pop(); }
                if (!methodSignature.ReturnVoid)
                {
                    int dest = DeclareLocal();
                    _parent.CallI(offset, tail, descriptorToken, dest, parameters);
                    Push(dest);
                }
                else
                {
                    _parent.CallI(offset, tail, descriptorToken, parameters);
                }
            }
            public override void LdStr(int offset, uint literalStringToken)
            {
                int dest = DeclareLocal();
                _parent.LdStr(offset, literalStringToken, dest);
                Push(dest);
            }
            public override void StSFld(int offset, bool volatilePrefix, uint fieldToken)
            {
                int value = Pop();
                _parent.StSFld(offset, volatilePrefix, fieldToken, value);
            }
            public override void LdSFld(int offset, bool volatilePrefix, uint fieldToken)
            {
                int dest = DeclareLocal();
                _parent.LdSFld(offset, volatilePrefix, fieldToken, dest);
                Push(dest);
            }
            public override void LdSFldA(int offset, uint fieldToken)
            {
                int dest = DeclareLocal();
                _parent.LdSFldA(offset, fieldToken, dest);
                Push(dest);
            }
            public override void LdFld(int offset, bool noTypeCheck, bool volatilePrefix, int alignment, uint fieldToken)
            {
                int obj = Pop();
                int dest = DeclareLocal();
                _parent.LdFld(offset, noTypeCheck, volatilePrefix, alignment, fieldToken, dest, obj);
                Push(dest);
            }
            public override void LdFldA(int offset, uint fieldToken)
            {
                int obj = Pop();
                int dest = DeclareLocal();
                _parent.LdFldA(offset, fieldToken, dest, obj);
                Push(dest);
            }
            public override void StFld(int offset, bool noNullCheck, bool volatilePrefix, int alignment, uint fieldToken)
            {
                int value = Pop();
                int obj = Pop();
                _parent.StFld(offset, noNullCheck, volatilePrefix, alignment, fieldToken, obj, value);
            }
            public override void LdArgA(int offset, int index)
            {
                int dest = DeclareLocal();
                _parent.LdArgA(offset, dest, index);
                Push(dest);
            }
            public override void IsInst(int offset, uint typeToken)
            {
                int value = Pop();
                int dest = DeclareLocal();
                _parent.IsInst(offset, typeToken, dest, value);
                Push(dest);
            }
            public override void CastClass(int offset, bool noTypeCheck, uint typeToken)
            {
                int value = Pop();
                int dest = DeclareLocal();
                _parent.CastClass(offset, noTypeCheck, typeToken, dest, value);
                Push(dest);
            }
            public override void Box(int offset, uint typeToken)
            {
                int src = Pop();
                int dest = DeclareLocal();
                _parent.Box(offset, typeToken, dest, src);
                Push(dest);
            }
            public override void UnboxAny(int offset, uint typeToken)
            {
                int src = Pop();
                int dest = DeclareLocal();
                _parent.UnboxAny(offset, typeToken, dest, src);
                Push(dest);
            }
            public override void Unbox(int offset, bool noTypeCheck, uint typeToken)
            {
                int src = Pop();
                int dest = DeclareLocal();
                _parent.Unbox(offset, noTypeCheck, typeToken, dest, src);
                Push(dest);
            }
            public override void NewArr(int offset, uint typeToken)
            {
                int size = Pop();
                int dest = DeclareLocal();
                _parent.NewArr(offset, typeToken, dest, size);
                Push(dest);
            }
            public override void NewObj(int offset, uint ctorToken)
            {
                Signature ctorSig = _signatures[ctorToken];
                int dest = DeclareLocal();
                int[] parameters = new int[ctorSig.ParametersCount];
                for (int n = parameters.Length - 1; n >= 0; n--) { parameters[n] = Pop(); }
                _parent.NewObj(offset, ctorToken, dest, parameters);
                Push(dest);
            }
            public override void InitObj(int offset, uint typeToken)
            {
                int dest = Pop();
                _parent.InitObj(offset, typeToken, dest);
            }
            public override void LdNull(int offset)
            {
                int dest = DeclareLocal();
                _parent.LdNull(offset, dest);
                Push(dest);
            }
            public override void LdIndI(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndI(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndI1(int offset, bool volatilePrefix)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndI1(offset, volatilePrefix, dest, address);
                Push(dest);
            }
            public override void LdIndI2(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndI2(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndI4(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndI4(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndI8(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndI8(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndU1(int offset, bool volatilePrefix)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndU1(offset, volatilePrefix, dest, address);
                Push(dest);
            }
            public override void LdIndU2(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndU2(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndU4(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndU4(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndR4(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndR4(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndR8(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndR8(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void LdIndRef(int offset, bool volatilePrefix, int alignment)
            {
                int address = Pop();
                int dest = DeclareLocal();
                _parent.LdIndRef(offset, volatilePrefix, alignment, dest, address);
                Push(dest);
            }
            public override void StIndRef(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndRef(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndI(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndI(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndI1(int offset, bool volatilePrefix)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndI1(offset, volatilePrefix,address, value);
            }
            public override void StIndI2(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndI2(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndI4(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndI4(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndI8(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndI8(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndR4(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndR4(offset, volatilePrefix, alignment, address, value);
            }
            public override void StIndR8(int offset, bool volatilePrefix, int alignment)
            {
                int value = Pop();
                int address = Pop();
                _parent.StIndR8(offset, volatilePrefix, alignment, address, value);
            }
            public override void LdVirtFtn(int offset, bool noNullCheck, uint methodToken)
            {
                int obj = Pop();
                int dest = DeclareLocal();
                _parent.LdVirtFtn(offset, noNullCheck, methodToken, dest, obj);
                Push(dest);
            }
            public override void LdElemI(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemI(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemI1(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemI1(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemI2(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemI2(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemI4(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemI4(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemI8(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemI8(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemU1(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemU1(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemU2(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemU2(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemU4(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemU4(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemR4(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemR4(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemR8(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemR8(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }
            public override void LdElemRef(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElemRef(offset, noNullCheck, noBoundCheck, dest, array, index);
                Push(dest);
            }

            public override void LdElem(int offset, bool noNullCheck, bool noBoundCheck, uint typeToken)
            {

                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();
                _parent.LdElem(offset, noNullCheck, noBoundCheck, typeToken, dest, array, index);
            }
            public override void LdElemA(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, bool readOnly, uint typeToken)
            {
                int index = Pop();
                int array = Pop();
                int dest = DeclareLocal();

                _parent.LdElemA(offset, noNullCheck, noTypeCheck, noBoundCheck, readOnly, typeToken, dest, array, index);
            }
            public override void StElem(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, uint typeToken)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElem(offset, noNullCheck, noTypeCheck, noBoundCheck, typeToken, array, index, value);
            }
            public override void StElemI1(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemI1(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemI2(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemI2(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemI4(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemI4(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemI8(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemI8(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemR4(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemR4(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void StElemR8(int offset, bool noNullCheck, bool noBoundCheck)
            {
                int index = Pop();
                int value = Pop();
                int array = Pop();
                _parent.StElemR8(offset, noNullCheck, noBoundCheck, array, index, value);
            }
            public override void LdFtn(int offset, uint methodToken)
            {
                int dest = DeclareLocal();
                _parent.LdFtn(offset, methodToken, dest);
                Push(dest);
            }
            public override void Switch(int offset, int[] addresses)
            {
                int value = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.Switch(offset, addresses, value);
            }
            public override void Br(int offset, int address)
            {
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.Br(offset, address);
            }
            public override void Leave(int offset, int address)
            {
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.Leave(offset, address);
            }
            public override void Rethrow(int offset)
            {
                _parent.Rethrow(offset);
            }
            public override void Throw(int offset)
            {
                int exception = Pop();
                _parent.Throw(offset, exception);
            }
            public override void Break(int offset)
            {
                _parent.Break(offset);
            }
            public override void BrTrue(int offset, int address)
            {
                int cond = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrTrue(offset, address, cond);
            }
            public override void BrFalse(int offset, int address)
            {
                int cond = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrFalse(offset, address, cond);
            }
            public override void BrEq(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrEq(offset, address, a, b);
            }
            public override void BrNeqUn(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrNeqUn(offset, address, a, b);
            }
            public override void BrLe(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrLe(offset, address, a, b);
            }
            public override void BrGe(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrGe(offset, address, a, b);
            }
            public override void BrGt(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrGt(offset, address, a, b);
            }
            public override void BrLt(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrLt(offset, address, a, b);
            }
            public override void BrGeUn(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrGeUn(offset, address, a, b);
            }
            public override void BrGtUn(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrGtUn(offset, address, a, b);
            }
            public override void BrLeUn(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrLeUn(offset, address, a, b);
            }
            public override void BrLtUn(int offset, int address)
            {
                int b = Pop();
                int a = Pop();
                BranchInformation.Information current = _branchInformation[offset];
                BranchInformation.Information target = _branchInformation[address];
                if (current.SaveStackSize > 0) { PopulateSaveSlot(offset); }
                _parent.BrLtUn(offset, address, a, b);
            }
            public override void CkFinite(int offset)
            {
                int a = Pop();
                int dest = DeclareLocal();
                _parent.CkFinite(offset, dest, a);
                Push(dest);
            }
            public override void ArgList(int offset)
            {
                int dest = DeclareLocal();
                _parent.ArgList(offset, dest);
                Push(dest);
            }
            public override void LdcI4(int offset, int constant)
            {
                int dest = DeclareLocal();
                _parent.LdcI4(offset, dest, constant);
                Push(dest);
            }
            public override void LdcI8(int offset, long constant)
            {
                int dest = DeclareLocal();
                _parent.LdcI8(offset, dest, constant);
                Push(dest);
            }
            public override void LdcR4(int offset, float constant)
            {
                int dest = DeclareLocal();
                _parent.LdcR4(offset, dest, constant);
                Push(dest);
            }
            public override void LdcR8(int offset, double constant)
            {
                int dest = DeclareLocal();
                _parent.LdcR8(offset, dest, constant);
                Push(dest);
            }
            public override void CopyObj(int offset, uint typeToken)
            {
                int src = Pop();
                int dest = Pop();

                _parent.CpObj(offset, dest, src);
            }
            public override void CpBlk(int offset, bool volatilePrefix, int alignment)
            {
                int size = Pop();
                int src = Pop();
                int dest = Pop();
                _parent.CpBlk(offset, volatilePrefix, alignment, dest, src, size);
            }
            public override void InitBlk(int offset, bool volatilePrefix, int alignment)
            {
                int size = Pop();
                int value = Pop();
                int dest = Pop();
                _parent.InitBlk(offset, volatilePrefix, alignment, dest, value, size);
            }
            public override void MkRefAny(int offset, uint token)
            {
                int src = Pop();
                int dest = Pop();
                _parent.MkRefAny(offset, token, dest, src);
                Push(dest);
            }
            public override void RefAnyType(int offset)
            {
                int src = Pop();
                int dest = Pop();
                _parent.RefAnyType(offset, dest, src);
                Push(dest);
            }
            public override void RefAnyVal(int offset, uint token)
            {
                int src = Pop();
                int dest = Pop();
                _parent.RefAnyVal(offset, token, dest, src);
                Push(dest);
            }
            public override void EndFilter(int offset)
            {
                int value = Pop();
                _parent.EndFilter(offset, value);
            }
            public override void Jmp(int offset, uint methodToken)
            {
                _parent.Jmp(offset, methodToken);
            }
            public override void Ret(int offset)
            {
                if (!_signature.ReturnVoid)
                {
                    int value = Pop();
                    _parent.Ret(offset, value);
                }
                else
                {
                    _parent.Ret(offset);
                }
            }

        }
    }
}
