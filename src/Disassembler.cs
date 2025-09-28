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
        abstract partial class Disassembler
        {
            bool _aborted = false;
            public void Abort() { _aborted = true; }
            public virtual void Fetch(int offset) { }
            public virtual void Nop(int offset) { }
            public virtual void ArgList(int offset) { }
            public virtual void Break(int offset) { }
            public virtual void LdArg(int offset, int index) { }
            public virtual void LdArgA(int offset, int index) { }
            public virtual void StArg(int offset, int index) { }
            public virtual void LdLoc(int offset, int index) { }
            public virtual void LdLocA(int offset, int index) { }
            public virtual void StLoc(int offset, int index) { }
            public virtual void LdNull(int offset) { }
            public virtual void NewObj(int offset, uint typeToken) { }
            public virtual void CopyObj(int offset, uint typeToken) { }
            public virtual void LdObj(int offset, bool volatilePrefix, int alignment, uint typeToken) { }
            public virtual void InitObj(int offset, uint typeToken) { }
            public virtual void NewArr(int offset, uint typeToken) { }
            public virtual void LdLen(int offset) { }
            public virtual void LdcI4(int offset, int constant) { }
            public virtual void LdcI8(int offset, long constant) { }
            public virtual void LdcR4(int offset, float constant) { }
            public virtual void LdcR8(int offset, double constant) { }
            public virtual void LdToken(int offset, uint token) { }
            public virtual void Dup(int offset) { }
            public virtual void Pop(int offset) { }
            public virtual void Jmp(int offset, uint methodToken) { }
            public virtual void Call(int offset, bool tail, uint methodToken) { }
            public virtual void Calli(int offset, bool tail, uint descriptorToken) { }
            public virtual void CallVirt(int offset, bool noNullCheck, uint constrainedType, bool tail, uint methodToken) { }
            public virtual void Ret(int offset) { }
            public virtual void Br(int offset, int address) { }
            public virtual void BrEq(int offset, int address) { }
            public virtual void BrNeqUn(int offset, int address) { }
            public virtual void BrGe(int offset, int address) { }
            public virtual void BrGeUn(int offset, int address) { }
            public virtual void BrGt(int offset, int address) { }
            public virtual void BrGtUn(int offset, int address) { }
            public virtual void BrLe(int offset, int address) { }
            public virtual void BrLeUn(int offset, int address) { }
            public virtual void BrLt(int offset, int address) { }
            public virtual void BrLtUn(int offset, int address) { }
            public virtual void BrFalse(int offset, int address) { }
            public virtual void BrTrue(int offset, int address) { }
            public virtual void Switch(int offset, int[] address) { }
            public virtual void Leave(int offset, int address) { }
            public virtual void EndFinally(int offset) { }
            public virtual void LdIndU1(int offset, bool volatilePrefix) { }
            public virtual void LdIndU2(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndU4(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndI1(int offset, bool volatilePrefix) { }
            public virtual void LdIndI2(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndI4(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndI8(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndI(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndR4(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndR8(int offset, bool volatilePrefix, int alignment) { }
            public virtual void LdIndRef(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndI(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndI1(int offset, bool volatilePrefix) { }
            public virtual void StIndI2(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndI4(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndI8(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndR4(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndR8(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StIndRef(int offset, bool volatilePrefix, int alignment) { }
            public virtual void StObj(int offset, bool volatilePrefix, int alignment, uint typeToken) { }
            public virtual void LdFld(int offset, bool noNullCheck, bool volatilePrefix, int alignment, uint fieldToken) { }
            public virtual void LdFldA(int offset, uint fieldToken) { }
            public virtual void LdSFld(int offset, bool volatilePrefix, uint fieldToken) { }
            public virtual void LdSFldA(int offset, uint fieldToken) { }
            public virtual void StFld(int offset, bool noNullCheck, bool volatilePrefix, int alignment, uint fieldToken) { }
            public virtual void StSFld(int offset, bool volatilePrefix, uint fieldToken) { }
            public virtual void Add(int offset) { }
            public virtual void AddOvf(int offset) { }
            public virtual void AddOvfUn(int offset) { }
            public virtual void Sub(int offset) { }
            public virtual void SubOvf(int offset) { }
            public virtual void SubOvfUn(int offset) { }
            public virtual void Mul(int offset) { }
            public virtual void MulOvf(int offset) { }
            public virtual void MulOvfUn(int offset) { }
            public virtual void Div(int offset) { }
            public virtual void DivUn(int offset) { }
            public virtual void Rem(int offset) { }
            public virtual void RemUn(int offset) { }
            public virtual void And(int offset) { }
            public virtual void Or(int offset) { }
            public virtual void Xor(int offset) { }
            public virtual void Shl(int offset) { }
            public virtual void Shr(int offset) { }
            public virtual void ShrUn(int offset) { }
            public virtual void Neg(int offset) { }
            public virtual void Not(int offset) { }
            public virtual void ConvU(int offset) { }
            public virtual void ConvU1(int offset) { }
            public virtual void ConvU2(int offset) { }
            public virtual void ConvU4(int offset) { }
            public virtual void ConvU8(int offset) { }
            public virtual void ConvI1(int offset) { }
            public virtual void ConvI2(int offset) { }
            public virtual void ConvI4(int offset) { }
            public virtual void ConvI8(int offset) { }
            public virtual void ConvR4(int offset) { }
            public virtual void ConvR8(int offset) { }
            public virtual void ConvRUn(int offset) { }
            public virtual void ConvI(int offset) { }
            public virtual void ConvOvfI1Un(int offset) { }
            public virtual void ConvOvfI2Un(int offset) { }
            public virtual void ConvOvfI4Un(int offset) { }
            public virtual void ConvOvfI8Un(int offset) { }
            public virtual void ConvOvfU1Un(int offset) { }
            public virtual void ConvOvfU2Un(int offset) { }
            public virtual void ConvOvfU4Un(int offset) { }
            public virtual void ConvOvfU8Un(int offset) { }
            public virtual void ConvOvfIUn(int offset) { }
            public virtual void ConvOvfUUn(int offset) { }
            public virtual void ConvOvfI(int offset) { }
            public virtual void ConvOvfU(int offset) { }
            public virtual void Ceq(int offset) { }
            public virtual void Cgt(int offset) { }
            public virtual void CgtUn(int offset) { }
            public virtual void Clt(int offset) { }
            public virtual void CltUn(int offset) { }
            public virtual void LocAlloc(int offset) { }
            public virtual void Throw(int offset) { }
            public virtual void LdElemA(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, bool readOnly, uint typeToken) { }
            public virtual void LdElem(int offset, bool noNullCheck, bool noBoundCheck, uint typeToken) { }
            public virtual void LdElemI1(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemI2(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemI4(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemI8(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemI(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemU1(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemU2(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemU4(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemR4(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemR8(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void LdElemRef(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElem(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, uint typeToken) { }
            public virtual void StElemI(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemI1(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemI2(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemI4(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemI8(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemR4(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemR8(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void StElemRef(int offset, bool noNullCheck, bool noBoundCheck) { }
            public virtual void ConvOvfI1(int offset) { }
            public virtual void ConvOvfU1(int offset) { }
            public virtual void ConvOvfI2(int offset) { }
            public virtual void ConvOvfU2(int offset) { }
            public virtual void ConvOvfI4(int offset) { }
            public virtual void ConvOvfU4(int offset) { }
            public virtual void ConvOvfI8(int offset) { }
            public virtual void ConvOvfU8(int offset) { }
            public virtual void LdStr(int offset, uint literalStringToken) { }
            public virtual void LdFtn(int offset, uint methodToken) { }
            public virtual void LdVirtFtn(int offset, bool noNullCheck, uint methodToken) { }
            public virtual void CpBlk(int offset, bool volatilePrefix, int alignment) { }
            public virtual void InitBlk(int offset, bool volatilePrefix, int alignment) { }
            public virtual void Box(int offset, uint typeToken) { }
            public virtual void Unbox(int offset, bool noTypeCheck, uint typeToken) { }
            public virtual void UnboxAny(int offset, uint typeToken) { }
            public virtual void IsInst(int offset, uint typeToken) { }
            public virtual void CastClass(int offset, bool noTypeCheck, uint typeToken) { }
            public virtual void SizeOf(int offset, uint typeToken) { }
            public virtual void Rethrow(int offset) { }
            public virtual void EndFilter(int offset) { }
            public virtual void RefAnyVal(int offset, uint token) { }
            public virtual void CkFinite(int offset) { }
            public virtual void MkRefAny(int offset, uint token) { }
            public virtual void RefAnyType(int offset) { }
            public Disassembler() { }
#if NET6_0_OR_GREATER
            /// <summary>
            /// Disassembles the provided IL bytecode starting from the given offset up to (but not including) the end index.
            /// </summary>
            /// <param name="bytecode">the block to disassemble</param>
            /// <param name="offset">the offset within the block where disassembly must begin</param>
            /// <param name="end">the end (exclusive) offset</param>
            public void Disassemble(Span<byte> bytecode, int offset, int end)
            {
                _aborted = false;
                bool volatilePrefix = false;
                bool unalignedPrefix = false;
                int alignment = 0;
                bool tailPrefix = false;
                bool readonlyPrefix = false;
                bool noNullCheck = false;
                bool noTypeCheck = false;
                bool noRangeCheck = false;
                uint constrainedType = 0;
                for (int n = offset; n < end;)
                {
                    int iloffset = n;
                    Fetch(iloffset);
                    if (_aborted) { return; }
                    switch (bytecode[n])
                    {
                        case 0x00 /* nop */: Nop(iloffset); n++; break;
                        case 0x01 /* break */: Break(iloffset); n++; break;
                        case 0x02 /* ldarg.0 */: LdArg(iloffset, 0); n++; break;
                        case 0x03 /* ldarg.1 */: LdArg(iloffset, 1); n++; break;
                        case 0x04 /* ldarg.2 */: LdArg(iloffset, 2); n++; break;
                        case 0x05 /* ldarg.3 */: LdArg(iloffset, 3); n++; break;
                        case 0x06 /* ldloc.0 */: LdLoc(iloffset, 0); n++; break;
                        case 0x07 /* ldloc.1 */: LdLoc(iloffset, 1); n++; break;
                        case 0x08 /* ldloc.2 */: LdLoc(iloffset, 2); n++; break;
                        case 0x09 /* ldloc.3 */: LdLoc(iloffset, 3); n++; break;
                        case 0x0A /* stloc.0 */: StLoc(iloffset, 0); n++; break;
                        case 0x0B /* stloc.1 */: StLoc(iloffset, 1); n++; break;
                        case 0x0C /* stloc.2 */: StLoc(iloffset, 2); n++; break;
                        case 0x0D /* stloc.3 */: StLoc(iloffset, 3); n++; break;
                        case 0x0E /* ldarg.S */: LdArg(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x0F /* ldarga.S */: LdArgA(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x10 /* starg.s */: StArg(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x11 /* ldloc.s */: LdLoc(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x12 /* ldloca.s */: LdLocA(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x13 /* stloc.s */: StLoc(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x14 /* ldnull */: LdNull(iloffset); n++; break;
                        case 0x15 /* ldc.i4.m1 */: LdcI4(iloffset, -1); n++; break;
                        case 0x16 /* ldc.i4.0 */: LdcI4(iloffset, 0); n++; break;
                        case 0x17 /* ldc.i4.1 */: LdcI4(iloffset, 1); n++; break;
                        case 0x18 /* ldc.i4.2 */: LdcI4(iloffset, 2); n++; break;
                        case 0x19 /* ldc.i4.3 */: LdcI4(iloffset, 3); n++; break;
                        case 0x1A /* ldc.i4.4 */: LdcI4(iloffset, 4); n++; break;
                        case 0x1B /* ldc.i4.5 */: LdcI4(iloffset, 5); n++; break;
                        case 0x1C /* ldc.i4.6 */: LdcI4(iloffset, 6); n++; break;
                        case 0x1D /* ldc.i4.7 */: LdcI4(iloffset, 7); n++; break;
                        case 0x1E /* ldc.i4.8 */: LdcI4(iloffset, 8); n++; break;
                        case 0x1F /* ldc.i4.s */: LdcI4(iloffset, (sbyte)bytecode[n + 1]); n += 2; break;
                        case 0x20 /* ldc.i4 */: LdcI4(iloffset, BitConverterLE.ToInt32(bytecode, n + 1)); n += 5; break;
                        case 0x21 /* ldc.i8 */: LdcI8(iloffset, BitConverterLE.ToInt64(bytecode, n + 1)); n += 9; break;
                        case 0x22 /* ldc.r4 */: LdcR4(iloffset, BitConverterLE.ToSingle(bytecode, n + 1)); n += 5; break;
                        case 0x23 /* ldc.r8 */: LdcR8(iloffset, BitConverterLE.ToDouble(bytecode, n + 1)); n += 9; break;
                        case 0x25 /* dup */: Dup(iloffset); n++; break;
                        case 0x26 /* pop */: Pop(iloffset); n++; break;
                        case 0x27 /* jmp */: Jmp(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x28 /* call */: Call(iloffset, tailPrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); tailPrefix = false; n += 5; break;
                        case 0x29 /* calli */: Calli(iloffset, tailPrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); tailPrefix = false; n += 5; break;
                        case 0x2A /* ret */: Ret(iloffset); n++; break;
                        case 0x2B /* br.s */: Br(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2C /* br.false.s */: BrFalse(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2D /* br.true.s */: BrTrue(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2E /* br.eq.s */: BrEq(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2F /* br.ge.s */: BrGe(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x30 /* br.gt.s */: BrGt(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x31 /* br.le.s */: BrLe(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x32 /* br.lt.s */: BrLt(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x33 /* br.neq.s */: BrNeqUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x34 /* br.ge.un.s */: BrGeUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x35 /* br.gt.un.s */: BrGtUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x36 /* br.le.un.s */: BrLeUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x37 /* br.lt.un.s */: BrLtUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x38 /* br */: Br(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x39 /* br.false */: BrFalse(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3A /* br.true */: BrTrue(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3B /* br.eq */: BrEq(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3C /* br.ge */: BrGe(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3D /* br.gt */: BrGt(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3E /* br.le */: BrLe(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3F /* br.lt */: BrLt(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x40 /* br.neq.un */: BrNeqUn(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x41 /* br.ge.un */: BrGeUn(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x42 /* br.gt.un */: BrGtUn(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x43 /* br.le.un */: BrLeUn(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x44 /* br.lt.un */: BrLtUn(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x45 /* switch */:
                            {
                                uint count = BitConverterLE.ToUInt32(bytecode, n + 1); n += 5;
                                int[] labels = new int[count];
                                for (int x = 0; x < labels.Length; x++)
                                {
                                    labels[x] = (int)(iloffset + BitConverterLE.ToInt32(bytecode, n) + (count * 4) + 5); n += 4;
                                }
                                Switch(iloffset, labels);
                                break;
                            }
                        case 0x46 /* ldind.i1 */: LdIndI1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x47 /* ldind.u1 */: LdIndU1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x48 /* ldind.i2 */: LdIndI2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x49 /* ldind.u2 */: LdIndU2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4A /* ldind.i4 */: LdIndI4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4B /* ldind.u4 */: LdIndU4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4C /* ldind.i8 */: LdIndI8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4D /* ldind.i */: LdIndI(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4E /* ldind.r4 */: LdIndR4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4F /* ldind.r8 */: LdIndR8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x50 /* ldind.ref */: LdIndRef(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x51 /* stind.ref */: StIndRef(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x52 /* stind.i1 */: StIndI1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x53 /* stind.i2 */: StIndI2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x54 /* stind.i4 */: StIndI4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x55 /* stind.i8 */: StIndI8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x56 /* stind.r4 */: StIndR4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x57 /* stind.r8 */: StIndR8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x58 /* add */: Add(iloffset); n++; break;
                        case 0x59 /* sub */: Sub(iloffset); n++; break;
                        case 0x5A /* mul */: Mul(iloffset); n++; break;
                        case 0x5B /* div */: Div(iloffset); n++; break;
                        case 0x5C /* div.un */: DivUn(iloffset); n++; break;
                        case 0x5D /* rem */: Rem(iloffset); n++; break;
                        case 0x5E /* rem.un */: RemUn(iloffset); n++; break;
                        case 0x5F /* and */: And(iloffset); n++; break;
                        case 0x60 /* or */: Or(iloffset); n++; break;
                        case 0x61 /* xor */: Xor(iloffset); n++; break;
                        case 0x62 /* shl */: Shl(iloffset); n++; break;
                        case 0x63 /* shr */: Shr(iloffset); n++; break;
                        case 0x64 /* shr.un */: ShrUn(iloffset); n++; break;
                        case 0x65 /* neg */: Neg(iloffset); n++; break;
                        case 0x66 /* not */: Not(iloffset); n++; break;
                        case 0x67 /* conv.i1 */: ConvI1(iloffset); n++; break;
                        case 0x68 /* conv.i2 */: ConvI2(iloffset); n++; break;
                        case 0x69 /* conv.i4 */: ConvI4(iloffset); n++; break;
                        case 0x6A /* conv.i8 */: ConvI8(iloffset); n++; break;
                        case 0x6B /* conv.r4 */: ConvR4(iloffset); n++; break;
                        case 0x6C /* conv.r8 */: ConvR8(iloffset); n++; break;
                        case 0x6D /* conv.u4 */: ConvU4(iloffset); n++; break;
                        case 0x6E /* conv.u8 */: ConvU8(iloffset); n++; break;
                        case 0x6F /* callvirt */: CallVirt(iloffset, noNullCheck, constrainedType, tailPrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); constrainedType = 0; noNullCheck = false; tailPrefix = false; n += 5; break;
                        case 0x70 /* copyobj */: CopyObj(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x71 /* ldobj */: LdObj(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverterLE.ToUInt32(bytecode, n + 1)); unalignedPrefix = false; volatilePrefix = false; n += 5; break;
                        case 0x72 /* ldstr */: LdStr(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x73 /* newobj */: NewObj(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x74 /* castclass */: CastClass(iloffset, noTypeCheck, BitConverterLE.ToUInt32(bytecode, n + 1)); noTypeCheck = false; n += 5; break;
                        case 0x75 /* isinst */: IsInst(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x76 /* conv.r.un */: ConvRUn(iloffset); n++; break;
                        case 0x79 /* unbox */: Unbox(iloffset, noTypeCheck, BitConverterLE.ToUInt32(bytecode, n + 1)); noTypeCheck = false; n += 5; break;
                        case 0x7A /* throw */: Throw(iloffset); n++; break;
                        case 0x7B /* ldfld */: LdFld(iloffset, noNullCheck, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; volatilePrefix = false; unalignedPrefix = false; n += 5; break;
                        case 0x7C /* ldflda */: LdFldA(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x7D /* stfld */: StFld(iloffset, noNullCheck, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; volatilePrefix = false; unalignedPrefix = false; n += 5; break;
                        case 0x7E /* ldsfld */: LdSFld(iloffset, volatilePrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); volatilePrefix = false; n += 5; break;
                        case 0x7F /* ldsflda */: LdSFldA(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x80 /* stsfld */: StSFld(iloffset, volatilePrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); volatilePrefix = false; n += 5; break;
                        case 0x81 /* stobj */: StObj(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverterLE.ToUInt32(bytecode, n + 1)); unalignedPrefix = false; volatilePrefix = false; n += 5; break;
                        case 0x82 /* conv.ovf.i1.un	*/: ConvOvfI1Un(iloffset); n++; break;
                        case 0x83 /* conv.ovf.i2.un	*/: ConvOvfI2Un(iloffset); n++; break;
                        case 0x84 /* conv.ovf.i4.un	*/: ConvOvfI4Un(iloffset); n++; break;
                        case 0x85 /* conv.ovf.i8.un	*/: ConvOvfI8Un(iloffset); n++; break;
                        case 0x86 /* conv.ovf.u1.un	*/: ConvOvfU1Un(iloffset); n++; break;
                        case 0x87 /* conv.ovf.u2.un	*/: ConvOvfU2Un(iloffset); n++; break;
                        case 0x88 /* conv.ovf.u4.un	*/: ConvOvfU4Un(iloffset); n++; break;
                        case 0x89 /* conv.ovf.u8.un	*/: ConvOvfU8Un(iloffset); n++; break;
                        case 0x8A /* conv.ovf.i.un	*/: ConvOvfIUn(iloffset); n++; break;
                        case 0x8B /* conv.ovf.u.un	*/: ConvOvfUUn(iloffset); n++; break;
                        case 0x8C /* box */: Box(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x8D /* newarr */: NewArr(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x8E /* ldlen */: LdLen(iloffset); n++; break;
                        case 0x8F /* ldelema  */: LdElemA(iloffset, noNullCheck, noTypeCheck, noRangeCheck, readonlyPrefix, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; noTypeCheck = false; noRangeCheck = false; readonlyPrefix = false; n += 5; break;
                        case 0x90 /* ldelem.i1 */: LdElemI1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x91 /* ldelem.u1 */: LdElemU1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x92 /* ldelem.i2 */: LdElemI2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x93 /* ldelem.u2 */: LdElemU2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x94 /* ldelem.i4 */: LdElemI4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x95 /* ldelem.u4 */: LdElemU4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x96 /* ldelem.i8 */: LdElemI8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x97 /* ldelem.i */: LdElemI(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x98 /* ldelem.r4 */: LdElemR4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x99 /* ldelem.r8 */: LdElemR8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9A /* ldelem.ref */: LdElemRef(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9B /* stelem.i */: StElemI(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9C /* stelem.i1 */: StElemI1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9D /* stelem.i2 */: StElemI2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9E /* stelem.i4 */: StElemI4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9F /* stelem.i8 */: StElemI8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA0 /* stelem.r4 */: StElemR4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA1 /* stelem.r8 */: StElemR8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA2 /* stelem.ref */: StElemRef(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA3 /* ldelem */: LdElem(iloffset, noNullCheck, noRangeCheck, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; noRangeCheck = false; n += 5; break;
                        case 0xA4 /* stelem */: StElem(iloffset, noNullCheck, noTypeCheck, noRangeCheck, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; noTypeCheck = false; noRangeCheck = false; n += 5; break;
                        case 0xA5 /* unbox.any*/: UnboxAny(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xB3: /* conv.ovf.i1 */ ConvOvfI1(iloffset); n++; break;
                        case 0xB4: /* conv.ovf.u1 */ ConvOvfU1(iloffset); n++; break;
                        case 0xB5: /* conv.ovf.i2 */ ConvOvfI2(iloffset); n++; break;
                        case 0xB6: /* conv.ovf.u2 */ ConvOvfU2(iloffset); n++; break;
                        case 0xB7: /* conv.ovf.i4 */ ConvOvfI4(iloffset); n++; break;
                        case 0xB8: /* conv.ovf.u4 */ ConvOvfU4(iloffset); n++; break;
                        case 0xB9: /* conv.ovf.i8 */ ConvOvfI8(iloffset); n++; break;
                        case 0xBA: /* conv.ovf.u8 */ ConvOvfU8(iloffset); n++; break;
                        case 0xC2: /* refanyval */ RefAnyVal(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xC3: /* ckfinite */ CkFinite(iloffset); n++; break;
                        case 0xC6: /* mkrefany */ MkRefAny(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xD0 /* ldtoken */: LdToken(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xD1 /* conv.u2 */: ConvU2(iloffset); n++; break;
                        case 0xD2 /* conv.u1 */: ConvU1(iloffset); n++; break;
                        case 0xD3 /* conv.i */: ConvI(iloffset); n++; break;
                        case 0xD4 /* conv.ovf.i */: ConvOvfI(iloffset); n++; break;
                        case 0xD5 /* conv.ovf.u */: ConvOvfU(iloffset); n++; break;
                        case 0xD6 /* add.ovf */: AddOvf(iloffset); n++; break;
                        case 0xD7 /* add.ovf.un */: AddOvfUn(iloffset); n++; break;
                        case 0xD8 /* mul.ovf */: MulOvf(iloffset); n++; break;
                        case 0xD9 /* mul.ovf.un */: MulOvfUn(iloffset); n++; break;
                        case 0xDA /* sub.ovf */: SubOvf(iloffset); n++; break;
                        case 0xDB /* sub.ovf.un */: SubOvfUn(iloffset); n++; break;
                        case 0xDC /* endfinally */: EndFinally(iloffset); n++; break;
                        case 0xDD /* leave */: Leave(iloffset, n + BitConverterLE.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0xDE /* leave.s */: Leave(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0xDF /* stind.i */: StIndI(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0xE0 /* conv.u */: ConvU(iloffset); n++; break;
                        case 0xFE:
                            n++;
                            switch (bytecode[n])
                            {
                                case 0x00 /* arglist */: ArgList(iloffset); n++; break;
                                case 0x01 /* ceq */: Ceq(iloffset); n++; break;
                                case 0x02 /* cgt */: Cgt(iloffset); n++; break;
                                case 0x03 /* cgt.un */: CgtUn(iloffset); n++; break;
                                case 0x04 /* clt */: Clt(iloffset); n++; break;
                                case 0x05 /* clt.un */: CltUn(iloffset); n++; break;
                                case 0x06 /* ldftn */: LdFtn(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x07 /* ldvirtftn */: LdVirtFtn(iloffset, noNullCheck, BitConverterLE.ToUInt32(bytecode, n + 1)); noNullCheck = false; n += 5; break;
                                case 0x09 /* ldarg */: LdArg(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0A /* ldarga */: LdArgA(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0B /* starg */: StArg(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0C /* ldloc */: LdLoc(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0D /* ldloca */: LdLocA(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0E /* stloc */: StLoc(iloffset, BitConverterLE.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0F /* LocAlloc */: LocAlloc(iloffset); n++; break;
                                case 0x11 /* endfilter */: EndFilter(iloffset); n++; break;
                                case 0x12 /* unaligned */:
                                    switch (bytecode[n + 1])
                                    {
                                        case 2: alignment = 2; break;
                                        case 4: alignment = 4; break;
                                        default: alignment = 1; break;
                                    }
                                    Nop(iloffset);
                                    unalignedPrefix = true;
                                    n += 2;
                                    break;
                                case 0x13 /* volatile */: Nop(iloffset); volatilePrefix = true; n++; break;
                                case 0x14 /* tail */: Nop(iloffset); tailPrefix = true; n++; break;
                                case 0x15 /* InitObj */: InitObj(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x16 /* constrained */: Nop(iloffset); constrainedType = BitConverterLE.ToUInt32(bytecode, n + 1); n += 5; break;
                                case 0x17 /* cpblk */: CpBlk(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                                case 0x18 /* initblk */: InitBlk(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                                case 0x19 /* no. */:
                                    if ((bytecode[n + 1] & 0x01) != 0) { noTypeCheck = true; }
                                    if ((bytecode[n + 1] & 0x02) != 0) { noRangeCheck = true; }
                                    if ((bytecode[n + 1] & 0x04) != 0) { noNullCheck = true; }
                                    n += 2;
                                    break;
                                case 0x1A /* rethrow */: Rethrow(iloffset); n++; break;
                                case 0x1C /* sizeof */: SizeOf(iloffset, BitConverterLE.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x1D /* refanytype*/: RefAnyType(iloffset); n++; break;
                                case 0x1E /* readonly */: Nop(iloffset); readonlyPrefix = true; n++; break;
                                default: throw new Exception("Unsupported CIL opcode 0xFE" + bytecode[n].ToString("X2"));
                            }
                            break;
                        default: throw new Exception("Unsupported CIL opcode 0x" + bytecode[n].ToString("X2"));
                    }
                    if (_aborted) { return; }
                }
            }
#endif
            /// <summary>
            /// Disassembles the provided IL bytecode starting from the given offset up to (but not including) the end index.
            /// </summary>
            /// <param name="bytecode">the block to disassemble</param>
            /// <param name="offset">the offset within the block where disassembly must begin</param>
            /// <param name="end">the end (exclusive) offset</param>
            public void Disassemble(byte[] bytecode, int offset, int end)
            {
                _aborted = false;
                bool volatilePrefix = false;
                bool unalignedPrefix = false;
                int alignment = 0;
                bool tailPrefix = false;
                bool readonlyPrefix = false;
                bool noNullCheck = false;
                bool noTypeCheck = false;
                bool noRangeCheck = false;
                uint constrainedType = 0;
                for (int n = offset; n < end;)
                {
                    int iloffset = n;
                    Fetch(iloffset);
                    if (_aborted) { return; }
                    switch (bytecode[n])
                    {
                        case 0x00 /* nop */: Nop(iloffset); n++; break;
                        case 0x01 /* break */: Break(iloffset); n++; break;
                        case 0x02 /* ldarg.0 */: LdArg(iloffset, 0); n++; break;
                        case 0x03 /* ldarg.1 */: LdArg(iloffset, 1); n++; break;
                        case 0x04 /* ldarg.2 */: LdArg(iloffset, 2); n++; break;
                        case 0x05 /* ldarg.3 */: LdArg(iloffset, 3); n++; break;
                        case 0x06 /* ldloc.0 */: LdLoc(iloffset, 0); n++; break;
                        case 0x07 /* ldloc.1 */: LdLoc(iloffset, 1); n++; break;
                        case 0x08 /* ldloc.2 */: LdLoc(iloffset, 2); n++; break;
                        case 0x09 /* ldloc.3 */: LdLoc(iloffset, 3); n++; break;
                        case 0x0A /* stloc.0 */: StLoc(iloffset, 0); n++; break;
                        case 0x0B /* stloc.1 */: StLoc(iloffset, 1); n++; break;
                        case 0x0C /* stloc.2 */: StLoc(iloffset, 2); n++; break;
                        case 0x0D /* stloc.3 */: StLoc(iloffset, 3); n++; break;
                        case 0x0E /* ldarg.S */: LdArg(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x0F /* ldarga.S */: LdArgA(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x10 /* starg.s */: StArg(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x11 /* ldloc.s */: LdLoc(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x12 /* ldloca.s */: LdLocA(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x13 /* stloc.s */: StLoc(iloffset, bytecode[n + 1]); n += 2; break;
                        case 0x14 /* ldnull */: LdNull(iloffset); n++; break;
                        case 0x15 /* ldc.i4.m1 */: LdcI4(iloffset, -1); n++; break;
                        case 0x16 /* ldc.i4.0 */: LdcI4(iloffset, 0); n++; break;
                        case 0x17 /* ldc.i4.1 */: LdcI4(iloffset, 1); n++; break;
                        case 0x18 /* ldc.i4.2 */: LdcI4(iloffset, 2); n++; break;
                        case 0x19 /* ldc.i4.3 */: LdcI4(iloffset, 3); n++; break;
                        case 0x1A /* ldc.i4.4 */: LdcI4(iloffset, 4); n++; break;
                        case 0x1B /* ldc.i4.5 */: LdcI4(iloffset, 5); n++; break;
                        case 0x1C /* ldc.i4.6 */: LdcI4(iloffset, 6); n++; break;
                        case 0x1D /* ldc.i4.7 */: LdcI4(iloffset, 7); n++; break;
                        case 0x1E /* ldc.i4.8 */: LdcI4(iloffset, 8); n++; break;
                        case 0x1F /* ldc.i4.s */: LdcI4(iloffset, (sbyte)bytecode[n + 1]); n += 2; break;
                        case 0x20 /* ldc.i4 */: LdcI4(iloffset, BitConverter.ToInt32(bytecode, n + 1)); n += 5; break;
                        case 0x21 /* ldc.i8 */: LdcI8(iloffset, BitConverter.ToInt64(bytecode, n + 1)); n += 9; break;
                        case 0x22 /* ldc.r4 */: LdcR4(iloffset, BitConverter.ToSingle(bytecode, n + 1)); n += 5; break;
                        case 0x23 /* ldc.r8 */: LdcR8(iloffset, BitConverter.ToDouble(bytecode, n + 1)); n += 9; break;
                        case 0x25 /* dup */: Dup(iloffset); n++; break;
                        case 0x26 /* pop */: Pop(iloffset); n++; break;
                        case 0x27 /* jmp */: Jmp(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x28 /* call */: Call(iloffset, tailPrefix, BitConverter.ToUInt32(bytecode, n + 1)); tailPrefix = false; n += 5; break;
                        case 0x29 /* calli */: Calli(iloffset, tailPrefix, BitConverter.ToUInt32(bytecode, n + 1)); tailPrefix = false; n += 5; break;
                        case 0x2A /* ret */: Ret(iloffset); n++; break;
                        case 0x2B /* br.s */: Br(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2C /* br.false.s */: BrFalse(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2D /* br.true.s */: BrTrue(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2E /* br.eq.s */: BrEq(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x2F /* br.ge.s */: BrGe(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x30 /* br.gt.s */: BrGt(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x31 /* br.le.s */: BrLe(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x32 /* br.lt.s */: BrLt(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x33 /* br.neq.s */: BrNeqUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x34 /* br.ge.un.s */: BrGeUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x35 /* br.gt.un.s */: BrGtUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x36 /* br.le.un.s */: BrLeUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x37 /* br.lt.un.s */: BrLtUn(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0x38 /* br */: Br(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x39 /* br.false */: BrFalse(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3A /* br.true */: BrTrue(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3B /* br.eq */: BrEq(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3C /* br.ge */: BrGe(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3D /* br.gt */: BrGt(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3E /* br.le */: BrLe(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x3F /* br.lt */: BrLt(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x40 /* br.neq.un */: BrNeqUn(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x41 /* br.ge.un */: BrGeUn(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x42 /* br.gt.un */: BrGtUn(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x43 /* br.le.un */: BrLeUn(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x44 /* br.lt.un */: BrLtUn(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0x45 /* switch */:
                            {
                                uint count = BitConverter.ToUInt32(bytecode, n + 1); n += 5;
                                int[] labels = new int[count];
                                for (int x = 0; x < labels.Length; x++)
                                {
                                    labels[x] = (int)(iloffset + BitConverter.ToInt32(bytecode, n) + (count * 4) + 5); n += 4;
                                }
                                Switch(iloffset, labels);
                                break;
                            }
                        case 0x46 /* ldind.i1 */: LdIndI1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x47 /* ldind.u1 */: LdIndU1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x48 /* ldind.i2 */: LdIndI2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x49 /* ldind.u2 */: LdIndU2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4A /* ldind.i4 */: LdIndI4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4B /* ldind.u4 */: LdIndU4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4C /* ldind.i8 */: LdIndI8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4D /* ldind.i */: LdIndI(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4E /* ldind.r4 */: LdIndR4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x4F /* ldind.r8 */: LdIndR8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x50 /* ldind.ref */: LdIndRef(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x51 /* stind.ref */: StIndRef(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x52 /* stind.i1 */: StIndI1(iloffset, volatilePrefix); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x53 /* stind.i2 */: StIndI2(iloffset, volatilePrefix, unalignedPrefix ? alignment : 2); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x54 /* stind.i4 */: StIndI4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x55 /* stind.i8 */: StIndI8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x56 /* stind.r4 */: StIndR4(iloffset, volatilePrefix, unalignedPrefix ? alignment : 4); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x57 /* stind.r8 */: StIndR8(iloffset, volatilePrefix, unalignedPrefix ? alignment : 8); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0x58 /* add */: Add(iloffset); n++; break;
                        case 0x59 /* sub */: Sub(iloffset); n++; break;
                        case 0x5A /* mul */: Mul(iloffset); n++; break;
                        case 0x5B /* div */: Div(iloffset); n++; break;
                        case 0x5C /* div.un */: DivUn(iloffset); n++; break;
                        case 0x5D /* rem */: Rem(iloffset); n++; break;
                        case 0x5E /* rem.un */: RemUn(iloffset); n++; break;
                        case 0x5F /* and */: And(iloffset); n++; break;
                        case 0x60 /* or */: Or(iloffset); n++; break;
                        case 0x61 /* xor */: Xor(iloffset); n++; break;
                        case 0x62 /* shl */: Shl(iloffset); n++; break;
                        case 0x63 /* shr */: Shr(iloffset); n++; break;
                        case 0x64 /* shr.un */: ShrUn(iloffset); n++; break;
                        case 0x65 /* neg */: Neg(iloffset); n++; break;
                        case 0x66 /* not */: Not(iloffset); n++; break;
                        case 0x67 /* conv.i1 */: ConvI1(iloffset); n++; break;
                        case 0x68 /* conv.i2 */: ConvI2(iloffset); n++; break;
                        case 0x69 /* conv.i4 */: ConvI4(iloffset); n++; break;
                        case 0x6A /* conv.i8 */: ConvI8(iloffset); n++; break;
                        case 0x6B /* conv.r4 */: ConvR4(iloffset); n++; break;
                        case 0x6C /* conv.r8 */: ConvR8(iloffset); n++; break;
                        case 0x6D /* conv.u4 */: ConvU4(iloffset); n++; break;
                        case 0x6E /* conv.u8 */: ConvU8(iloffset); n++; break;
                        case 0x6F /* callvirt */: CallVirt(iloffset, noNullCheck, constrainedType, tailPrefix, BitConverter.ToUInt32(bytecode, n + 1)); constrainedType = 0; noNullCheck = false; tailPrefix = false; n += 5; break;
                        case 0x70 /* copyobj */: CopyObj(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x71 /* ldobj */: LdObj(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverter.ToUInt32(bytecode, n + 1)); unalignedPrefix = false; volatilePrefix = false; n += 5; break;
                        case 0x72 /* ldstr */: LdStr(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x73 /* newobj */: NewObj(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x74 /* castclass */: CastClass(iloffset, noTypeCheck, BitConverter.ToUInt32(bytecode, n + 1)); noTypeCheck = false; n += 5; break;
                        case 0x75 /* isinst */: IsInst(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x76 /* conv.r.un */: ConvRUn(iloffset); n++; break;
                        case 0x79 /* unbox */: Unbox(iloffset, noTypeCheck, BitConverter.ToUInt32(bytecode, n + 1)); noTypeCheck = false; n += 5; break;
                        case 0x7A /* throw */: Throw(iloffset); n++; break;
                        case 0x7B /* ldfld */: LdFld(iloffset, noNullCheck, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; volatilePrefix = false; unalignedPrefix = false; n += 5; break;
                        case 0x7C /* ldflda */: LdFldA(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x7D /* stfld */: StFld(iloffset, noNullCheck, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; volatilePrefix = false; unalignedPrefix = false; n += 5; break;
                        case 0x7E /* ldsfld */: LdSFld(iloffset, volatilePrefix, BitConverter.ToUInt32(bytecode, n + 1)); volatilePrefix = false; n += 5; break;
                        case 0x7F /* ldsflda */: LdSFldA(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x80 /* stsfld */: StSFld(iloffset, volatilePrefix, BitConverter.ToUInt32(bytecode, n + 1)); volatilePrefix = false; n += 5; break;
                        case 0x81 /* stobj */: StObj(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1, BitConverter.ToUInt32(bytecode, n + 1)); unalignedPrefix = false; volatilePrefix = false; n += 5; break;
                        case 0x82 /* conv.ovf.i1.un	*/: ConvOvfI1Un(iloffset); n++; break;
                        case 0x83 /* conv.ovf.i2.un	*/: ConvOvfI2Un(iloffset); n++; break;
                        case 0x84 /* conv.ovf.i4.un	*/: ConvOvfI4Un(iloffset); n++; break;
                        case 0x85 /* conv.ovf.i8.un	*/: ConvOvfI8Un(iloffset); n++; break;
                        case 0x86 /* conv.ovf.u1.un	*/: ConvOvfU1Un(iloffset); n++; break;
                        case 0x87 /* conv.ovf.u2.un	*/: ConvOvfU2Un(iloffset); n++; break;
                        case 0x88 /* conv.ovf.u4.un	*/: ConvOvfU4Un(iloffset); n++; break;
                        case 0x89 /* conv.ovf.u8.un	*/: ConvOvfU8Un(iloffset); n++; break;
                        case 0x8A /* conv.ovf.i.un	*/: ConvOvfIUn(iloffset); n++; break;
                        case 0x8B /* conv.ovf.u.un	*/: ConvOvfUUn(iloffset); n++; break;
                        case 0x8C /* box */: Box(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x8D /* newarr */: NewArr(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0x8E /* ldlen */: LdLen(iloffset); n++; break;
                        case 0x8F /* ldelema  */: LdElemA(iloffset, noNullCheck, noTypeCheck, noRangeCheck, readonlyPrefix, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; noTypeCheck = false; noRangeCheck = false; readonlyPrefix = false; n += 5; break;
                        case 0x90 /* ldelem.i1 */: LdElemI1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x91 /* ldelem.u1 */: LdElemU1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x92 /* ldelem.i2 */: LdElemI2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x93 /* ldelem.u2 */: LdElemU2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x94 /* ldelem.i4 */: LdElemI4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x95 /* ldelem.u4 */: LdElemU4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x96 /* ldelem.i8 */: LdElemI8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x97 /* ldelem.i */: LdElemI(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x98 /* ldelem.r4 */: LdElemR4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x99 /* ldelem.r8 */: LdElemR8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9A /* ldelem.ref */: LdElemRef(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9B /* stelem.i */: StElemI(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9C /* stelem.i1 */: StElemI1(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9D /* stelem.i2 */: StElemI2(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9E /* stelem.i4 */: StElemI4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0x9F /* stelem.i8 */: StElemI8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA0 /* stelem.r4 */: StElemR4(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA1 /* stelem.r8 */: StElemR8(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA2 /* stelem.ref */: StElemRef(iloffset, noNullCheck, noRangeCheck); noNullCheck = false; noRangeCheck = false; n++; break;
                        case 0xA3 /* ldelem */: LdElem(iloffset, noNullCheck, noRangeCheck, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; noRangeCheck = false; n += 5; break;
                        case 0xA4 /* stelem */: StElem(iloffset, noNullCheck, noTypeCheck, noRangeCheck, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; noTypeCheck = false; noRangeCheck = false; n += 5; break;
                        case 0xA5 /* unbox.any*/: UnboxAny(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xB3: /* conv.ovf.i1 */ ConvOvfI1(iloffset); n++; break;
                        case 0xB4: /* conv.ovf.u1 */ ConvOvfU1(iloffset); n++; break;
                        case 0xB5: /* conv.ovf.i2 */ ConvOvfI2(iloffset); n++; break;
                        case 0xB6: /* conv.ovf.u2 */ ConvOvfU2(iloffset); n++; break;
                        case 0xB7: /* conv.ovf.i4 */ ConvOvfI4(iloffset); n++; break;
                        case 0xB8: /* conv.ovf.u4 */ ConvOvfU4(iloffset); n++; break;
                        case 0xB9: /* conv.ovf.i8 */ ConvOvfI8(iloffset); n++; break;
                        case 0xBA: /* conv.ovf.u8 */ ConvOvfU8(iloffset); n++; break;
                        case 0xC2: /* refanyval */ RefAnyVal(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xC3: /* ckfinite */ CkFinite(iloffset); n++; break;
                        case 0xC6: /* mkrefany */ MkRefAny(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xD0 /* ldtoken */: LdToken(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                        case 0xD1 /* conv.u2 */: ConvU2(iloffset); n++; break;
                        case 0xD2 /* conv.u1 */: ConvU1(iloffset); n++; break;
                        case 0xD3 /* conv.i */: ConvI(iloffset); n++; break;
                        case 0xD4 /* conv.ovf.i */: ConvOvfI(iloffset); n++; break;
                        case 0xD5 /* conv.ovf.u */: ConvOvfU(iloffset); n++; break;
                        case 0xD6 /* add.ovf */: AddOvf(iloffset); n++; break;
                        case 0xD7 /* add.ovf.un */: AddOvfUn(iloffset); n++; break;
                        case 0xD8 /* mul.ovf */: MulOvf(iloffset); n++; break;
                        case 0xD9 /* mul.ovf.un */: MulOvfUn(iloffset); n++; break;
                        case 0xDA /* sub.ovf */: SubOvf(iloffset); n++; break;
                        case 0xDB /* sub.ovf.un */: SubOvfUn(iloffset); n++; break;
                        case 0xDC /* endfinally */: EndFinally(iloffset); n++; break;
                        case 0xDD /* leave */: Leave(iloffset, n + BitConverter.ToInt32(bytecode, n + 1) + 5); n += 5; break;
                        case 0xDE /* leave.s */: Leave(iloffset, n + (sbyte)bytecode[n + 1] + 2); n += 2; break;
                        case 0xDF /* stind.i */: StIndI(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); unalignedPrefix = false; volatilePrefix = false; n++; break;
                        case 0xE0 /* conv.u */: ConvU(iloffset); n++; break;
                        case 0xFE:
                            n++;
                            switch (bytecode[n])
                            {
                                case 0x00 /* arglist */: ArgList(iloffset); n++; break;
                                case 0x01 /* ceq */: Ceq(iloffset); n++; break;
                                case 0x02 /* cgt */: Cgt(iloffset); n++; break;
                                case 0x03 /* cgt.un */: CgtUn(iloffset); n++; break;
                                case 0x04 /* clt */: Clt(iloffset); n++; break;
                                case 0x05 /* clt.un */: CltUn(iloffset); n++; break;
                                case 0x06 /* ldftn */: LdFtn(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x07 /* ldvirtftn */: LdVirtFtn(iloffset, noNullCheck, BitConverter.ToUInt32(bytecode, n + 1)); noNullCheck = false; n += 5; break;
                                case 0x09 /* ldarg */: LdArg(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0A /* ldarga */: LdArgA(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0B /* starg */: StArg(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0C /* ldloc */: LdLoc(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0D /* ldloca */: LdLocA(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0E /* stloc */: StLoc(iloffset, BitConverter.ToUInt16(bytecode, n + 1)); n += 3; break;
                                case 0x0F /* LocAlloc */: LocAlloc(iloffset); n++; break;
                                case 0x11 /* endfilter */: EndFilter(iloffset); n++; break;
                                case 0x12 /* unaligned */:
                                    switch (bytecode[n + 1])
                                    {
                                        case 2: alignment = 2; break;
                                        case 4: alignment = 4; break;
                                        default: alignment = 1; break;
                                    }
                                    Nop(iloffset);
                                    unalignedPrefix = true;
                                    n += 2;
                                    break;
                                case 0x13 /* volatile */: Nop(iloffset); volatilePrefix = true; n++; break;
                                case 0x14 /* tail */: Nop(iloffset); tailPrefix = true; n++; break;
                                case 0x15 /* InitObj */: InitObj(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x16 /* constrained */: Nop(iloffset); constrainedType = BitConverter.ToUInt32(bytecode, n + 1); n += 5; break;
                                case 0x17 /* cpblk */: CpBlk(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); volatilePrefix = false; unalignedPrefix = false; n++; break;
                                case 0x18 /* initblk */: InitBlk(iloffset, volatilePrefix, unalignedPrefix ? alignment : -1); volatilePrefix = false; unalignedPrefix = false; n++; break;
                                case 0x19 /* no. */:
                                    if ((bytecode[n + 1] & 0x01) != 0) { noTypeCheck = true; }
                                    if ((bytecode[n + 1] & 0x02) != 0) { noRangeCheck = true; }
                                    if ((bytecode[n + 1] & 0x04) != 0) { noNullCheck = true; }
                                    n += 2;
                                    break;
                                case 0x1A /* rethrow */: Rethrow(iloffset); n++; break;
                                case 0x1C /* sizeof */: SizeOf(iloffset, BitConverter.ToUInt32(bytecode, n + 1)); n += 5; break;
                                case 0x1D /* refanytype*/: RefAnyType(iloffset); n++; break;
                                case 0x1E /* readonly */: Nop(iloffset); readonlyPrefix = true; n++; break;
                                default: throw new Exception("Unsupported CIL opcode 0xFE" + bytecode[n].ToString("X2"));
                            }
                            break;
                        default: throw new Exception("Unsupported CIL opcode 0x" + bytecode[n].ToString("X2"));
                    }
                    if (_aborted) { return; }
                }
            }
        }
    }
}