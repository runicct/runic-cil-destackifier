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
        public virtual void Nop(int offset) { }
        public virtual void Add(int offset, int destination, int a, int b) { }
        public virtual void AddOvf(int offset, int destination, int a, int b) { }
        public virtual void AddOvfUn(int offset, int destination, int a, int b) { }
        public virtual void Sub(int offset, int destination, int a, int b) { }
        public virtual void SubOvf(int offset, int destination, int a, int b) { }
        public virtual void SubOvfUn(int offset, int destination, int a, int b) { }
        public virtual void Mul(int offset, int destination, int a, int b) { }
        public virtual void MulOvf(int offset, int destination, int a, int b) { }
        public virtual void MulOvfUn(int offset, int destination, int a, int b) { }
        public virtual void Div(int offset, int destination, int a, int b) { }
        public virtual void DivUn(int offset, int destination, int a, int b) { }
        public virtual void Rem(int offset, int destination, int a, int b) { }
        public virtual void RemUn(int offset, int destination, int a, int b) { }
        public virtual void Neg(int offset, int destination, int source) { }
        public virtual void And(int offset, int destination, int a, int b) { }
        public virtual void Or(int offset, int destination, int a, int b) { }
        public virtual void Xor(int offset, int destination, int a, int b) { }
        public virtual void Shr(int offset, int destination, int a, int b) { }
        public virtual void ShrUn(int offset, int destination, int a, int b) { }
        public virtual void Shl(int offset, int destination, int a, int b) { }
        public virtual void Not(int offset, int destination, int source) { }
        public virtual void SizeOf(int offset, uint typeToken, int destination) { }
        public virtual void StLoc(int offset, int destination, int source) { }
        public virtual void LdLocA(int offset, int destination, int index) { }
        public virtual void LdArg(int offset, int destination, int index) { }
        public virtual void LdArgA(int offset, int destination, int index) { }
        public virtual void StArg(int offset, int index, int source) { }
        public virtual void LdObj(int offset, bool volatilePrefix, int alignment, uint typeToken, int destination, int address) { }
        public virtual void StObj(int offset, bool volatilePrefix, int alignment, uint typeToken, int address, int value) { }
        public virtual void LdToken(int offset, uint token, int destination) { }
        public virtual void Br(int offset, int address) { }
        public virtual void BrTrue(int offset, int address, int condition) { }
        public virtual void BrFalse(int offset, int address, int condition) { }
        public virtual void BrEq(int offset, int address, int a, int b) { }
        public virtual void BrNeqUn(int offset, int address, int a, int b) { }
        public virtual void BrLe(int offset, int address, int a, int b) { }
        public virtual void BrGe(int offset, int address, int a, int b) { }
        public virtual void BrGt(int offset, int address, int a, int b) { }
        public virtual void BrLt(int offset, int address, int a, int b) { }
        public virtual void BrGeUn(int offset, int address, int a, int b) { }
        public virtual void BrGtUn(int offset, int address, int a, int b) { }
        public virtual void BrLeUn(int offset, int address, int a, int b) { }
        public virtual void BrLtUn(int offset, int address, int a, int b) { }
        public virtual void Ceq(int offset, int destination, int a, int b) { }
        public virtual void Cgt(int offset, int destination, int a, int b) { }
        public virtual void CgtUn(int offset, int destination, int a, int b) { }
        public virtual void Clt(int offset, int destination, int a, int b) { }
        public virtual void CltUn(int offset, int destination, int a, int b) { }
        public virtual void Switch(int offset, int[] addresses, int value) { }
        public virtual void LdcI4(int offset, int destination, int value) { }
        public virtual void LdcI8(int offset, int destination, long value) { }
        public virtual void LdcR4(int offset, int destination, float value) { }
        public virtual void LdcR8(int offset, int destination, double value) { }
        public virtual void StIndRef(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndI(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndI1(int offset, bool volatilePrefix, int address, int value) { }
        public virtual void StIndI2(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndI4(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndI8(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndR4(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void StIndR8(int offset, bool volatilePrefix, int alignment, int address, int value) { }
        public virtual void LdIndI(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndI1(int offset, bool volatilePrefix, int destination, int address) { }
        public virtual void LdIndI2(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndI4(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndI8(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndU1(int offset, bool volatilePrefix, int destination, int address) { }
        public virtual void LdIndU2(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndU4(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndR4(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndR8(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdIndRef(int offset, bool volatilePrefix, int alignment, int destination, int address) { }
        public virtual void LdLen(int offset, int destination, int array) { }
        public virtual void LdElemI(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemI1(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemI2(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemI4(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemI8(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemU1(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemU2(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemU4(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemR4(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemR8(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElemRef(int offset, bool noNullCheck, bool noBoundCheck, int destination, int array, int index) { }
        public virtual void LdElem(int offset, bool noNullCheck, bool noBoundCheck, uint typeToken, int destination, int array, int index) { }
        public virtual void LdElemA(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, bool readOnly, uint typeToken, int destination, int array, int index) { }
        public virtual void StElem(int offset, bool noNullCheck, bool noTypeCheck, bool noBoundCheck, uint typeToken, int array, int index, int value) { }
        public virtual void StElemI(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemI1(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemI2(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemI4(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemI8(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemR4(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemR8(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void StElemRef(int offset, bool noNullCheck, bool noBoundCheck, int array, int index, int value) { }
        public virtual void ConvU(int offset, int destination, int source) { }
        public virtual void ConvI(int offset, int destination, int source) { }
        public virtual void ConvI1(int offset, int destination, int source) { }
        public virtual void ConvI2(int offset, int destination, int source) { }
        public virtual void ConvI4(int offset, int destination, int source) { }
        public virtual void ConvI8(int offset, int destination, int source) { }
        public virtual void ConvU1(int offset, int destination, int source) { }
        public virtual void ConvU2(int offset, int destination, int source) { }
        public virtual void ConvU4(int offset, int destination, int source) { }
        public virtual void ConvU8(int offset, int destination, int source) { }
        public virtual void ConvR4(int offset, int destination, int source) { }
        public virtual void ConvR8(int offset, int destination, int source) { }
        public virtual void ConvOvfI(int offset, int destination, int source) { }
        public virtual void ConvOvfI1(int offset, int destination, int source) { }
        public virtual void ConvOvfI2(int offset, int destination, int source) { }
        public virtual void ConvOvfI4(int offset, int destination, int source) { }
        public virtual void ConvOvfI8(int offset, int destination, int source) { }
        public virtual void ConvOvfU(int offset, int destination, int source) { }
        public virtual void ConvOvfU1(int offset, int destination, int source) { }
        public virtual void ConvOvfU2(int offset, int destination, int source) { }
        public virtual void ConvOvfU4(int offset, int destination, int source) { }
        public virtual void ConvOvfU8(int offset, int destination, int source) { }
        public virtual void ConvOvfIUn(int offset, int destination, int source) { }
        public virtual void ConvOvfI1Un(int offset, int destination, int source) { }
        public virtual void ConvOvfI2Un(int offset, int destination, int source) { }
        public virtual void ConvOvfI4Un(int offset, int destination, int source) { }
        public virtual void ConvOvfI8Un(int offset, int destination, int source) { }
        public virtual void ConvOvfUUn(int offset, int destination, int source) { }
        public virtual void ConvOvfU1Un(int offset, int destination, int source) { }
        public virtual void ConvOvfU2Un(int offset, int destination, int source) { }
        public virtual void ConvOvfU4Un(int offset, int destination, int source) { }
        public virtual void ConvOvfU8Un(int offset, int destination, int source) { }
        public virtual void ConvRUn(int offset, int destination, int source) { }
        public virtual void LocAlloc(int offset, int destination, int size) { }
        public virtual void LdFtn(int offset, uint methodToken, int destination) { }
        public virtual void CastClass(int offset, bool noTypeCheck, uint typeToken, int destination, int value) { }
        public virtual void EndFinally(int offset) { }
        public virtual void LdStr(int offset, uint literalStringToken, int destination) { }
        public virtual void StSFld(int offset, uint fieldToken, int value) { }
        public virtual void LdSFld(int offset, uint fieldToken, int destination) { }
        public virtual void LdSFldA(int offset, uint fieldToken, int destination) { }
        public virtual void LdFld(int offset, bool noTypeCheck, bool volatilePrefix, int alignment, uint fieldToken, int destination, int obj) { }
        public virtual void LdFldA(int offset, uint fieldToken, int destination, int obj) { }
        public virtual void StFld(int offset, bool noNullCheck, bool volatilePrefix, int alignment, uint fieldToken, int obj, int value) { }
        public virtual void StSFld(int offset, bool volatilePrefix, uint fieldToken, int value) { }
        public virtual void LdSFld(int offset, bool volatilePrefix, uint fieldToken, int destination) { }
        public virtual void IsInst(int offset, uint typeToken, int destination, int value) { }
        public virtual void Box(int offset, uint typeToken, int destination, int source) { }
        public virtual void Unbox(int offset, bool noTypeCheck, uint typeToken, int destination, int source) { }
        public virtual void UnboxAny(int offset, uint typeToken, int destination, int source) { }
        public virtual void NewArr(int offset, uint typeToken, int destination, int size) { }
        public virtual void NewObj(int offset, uint ctorToken, int destination, int[] parameters) { }
        public virtual void CallVirt(int offset, bool noNullCheck, bool tail, uint methodToken, int destination, int[] parameters) { }
        public virtual void CallVirt(int offset, bool noNullCheck, bool tail, uint methodToken, int[] parameters) { }
        public virtual void CallI(int offset, bool tail, uint signatureToken, int destination, int[] parameters) { }
        public virtual void CallI(int offset, bool tail, uint signatureToken, int[] parameters) { }
        public virtual void Call(int offset, bool tail, uint methodToken, int destination, int[] parameters) { }
        public virtual void Call(int offset, bool tail, uint methodToken, int[] parameters) { }
        public virtual void InitObj(int offset, uint typeToken, int destination) { }
        public virtual void ArgList(int offset, int destination) { }
        public virtual void CkFinite(int offset, int destination, int source) { }
        public virtual void LdVirtFtn(int offset, bool noNullCheck, uint methodToken, int destination, int obj) { }
        public virtual void LdNull(int offset, int destination) { }
        public virtual void CpObj(int offset, int destination, int source) { }
        public virtual void CpBlk(int offset,  bool volatilePrefix, int alignment, int destination, int source, int size) { }
        public virtual void InitBlk(int offset, bool volatilePrefix, int alignment, int destination, int value, int size) { }
        public virtual void Break(int offset) { }
        public virtual void Ret(int offset, int value) { }
        public virtual void Ret(int offset) { }
        public virtual void Leave(int offset, int address) { }
        public virtual void Rethrow(int offset) { }
        public virtual void Throw(int offset, int exception) { }
        public virtual void MkRefAny(int offset, uint token, int destination, int source) { }
        public virtual void RefAnyType(int offset, int destination, int source) { }
        public virtual void RefAnyVal(int offset, uint token, int destination, int source) { }
        public virtual void EndFilter(int offset, int value) { }
        public virtual void Jmp(int offset, uint methodToken) { }
#if NET6_0_OR_GREATER
        public void Destackify(uint methodToken, Span<byte> bytecode) { Destackify(GetMethodSignature(methodToken), bytecode); }
        public void Destackify(byte[] methodSignature, Span<byte> bytecode)
        {
            SignatureCollector signatureCollector = new SignatureCollector(this);
            Signature signature = new Signature(this, methodSignature);
            Dictionary<uint, Signature> methodSignatures = signatureCollector.Process(bytecode);
            LocalCollector localCollector = new LocalCollector();
            HashSet<int> locals = localCollector.Process(bytecode);
            BranchInformation branchInformation = new BranchInformation(methodSignatures);
            branchInformation.Process(bytecode);
            DestackifyDisassembler disassembler = new DestackifyDisassembler(branchInformation, methodSignatures, locals, (uint)branchInformation.MaxStackSize, signature, this);
            disassembler.Process(bytecode);
        }
#endif
        public void Destackify(uint methodToken, byte[] bytecode) { Destackify(GetMethodSignature(methodToken), bytecode); }
        public void Destackify(byte[] methodSignature, byte[] bytecode)
        {
            SignatureCollector signatureCollector = new SignatureCollector(this);
            Signature signature = new Signature(this, methodSignature);
            Dictionary<uint, Signature> methodSignatures = signatureCollector.Process(bytecode);
            LocalCollector localCollector = new LocalCollector();
            HashSet<int> locals = localCollector.Process(bytecode);
            BranchInformation branchInformation = new BranchInformation(methodSignatures);
            branchInformation.Process(bytecode);
            DestackifyDisassembler disassembler = new DestackifyDisassembler(branchInformation, methodSignatures, locals, (uint)branchInformation.MaxStackSize, signature, this);
            disassembler.Process(bytecode);
        }
    }
}
