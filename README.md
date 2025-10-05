# runic-cil-destackifier

## Introduction
From an API perspective Runic.CIL.Destackifier provides a similar interface as Runic.CIL.Disassembler. However unlike the Disassembler which provides a raw IL stream (as is) the destackifier removes the use of the stack and instead relies on locals. This makes it more suitable for use in translators and jitters.

For instance, the following code:
```
IL_00: LdCI4 1
IL_01: LdCI4 2
IL_02: Add
IL_03: StLoc %0
```

Will trigger the following callbacks with Runic.CIL.Disassembler:
```
LdCI4(/* offset: */ 0, 1)
LdCI4(/* offset: */ 1, 2)
Add(/* offset: */, 2)
StLoc(/* offset: */ 3, %0)
```

And the following callbacks with Runic.CIL.Destackifier:
```
LdCI4(/* offset: */ 0,  %1, 1)
LdCI4(/* offset: */ 1, %2, 2)
Add(/* offset: */ 2, %3, %1, %2)
StLoc(/* offset: */ 3, %0, %3)
```
