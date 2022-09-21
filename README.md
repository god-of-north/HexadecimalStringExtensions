 [nuget-url]: https://www.nuget.org/packages/GON.HexadecimalStringExtensions/

# GON.HexadecimalStringExtensions

## What is does?

.NET Extensions to simplify work with hexadecimal strings


## Nuget Package

[NuGet package link][nuget-url]


## Examples

```csharp
//String -> Byte Array
byte[] data1 = "00112233445566778899AABBCCDDEEFF".bin();
byte[] data2 = "001122 33445566 778899AABB CCDDEEFF".bin();
byte[] data3 = "0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF".bin();

//String -> Byte Array
string str = new byte[] { 0x01, 0xFF, 0x22, 0xFA}.hex();

//XOR
string xored = "0011223344556677".XOR("FFEEDDCCBBAA9988");

//Compare Byte Arrays
byte[] a = "FF0000".bin();
byte[] b = "FF0001".bin();
if (a.Compare(b))
{
    ...
}
```


