﻿using Microsoft.VisualBasic;
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
        class LocalCollector : Disassembler
        {
            HashSet<int> _locals = new HashSet<int>();
            public override void LdLoc(int offset, int index) { _locals.Add(index); }
            public override void LdLocA(int offset, int index) { _locals.Add(index); }
            public override void StLoc(int offset, int index) { _locals.Add(index); }
#if NET6_0_OR_GREATER
            public HashSet<int> Process(Span<byte> bytecode)
            {
                Disassemble(bytecode, 0, bytecode.Length);
                return _locals;
            }
#endif
            public HashSet<int> Process(byte[] bytecode)
            {
                Disassemble(bytecode, 0, bytecode.Length);
                return _locals;
            }
        }
    }
}
