using System.IO;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;

public static class ByteArrayEx
{
    private static SymmetricAlgorithm aes = new RijndaelManaged();

    static ByteArrayEx()
    {
        aes.Key = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        aes.Padding = PaddingMode.PKCS7;
    }

    public static IEnumerable<byte> Decompress(this IEnumerable<byte> data)
    {
        byte[] output = data.ToArray();
        using (var inputStream = new MemoryStream(output))
        using (var decompressor = new DeflateStream(inputStream, CompressionMode.Decompress))
        using (var decompressStream = new MemoryStream())
        {
            decompressor.CopyTo(decompressStream);
            decompressor.Close();
            output = decompressStream.ToArray();
        }

        return output;
    }

    public static IEnumerable<byte> Compress(this IEnumerable<byte> data)
    {
        byte[] output = null;
        using (var inputStream = new MemoryStream(data.ToArray()))
        using (var compressStream = new MemoryStream())
        using (var compressor = new DeflateStream(compressStream, CompressionMode.Compress))
        {
            inputStream.CopyTo(compressor);
            compressor.Close();
            output = compressStream.ToArray();
        }

        return output;
    }

    public static IEnumerable<byte> Decrypt(this IEnumerable<byte> data)
    {
        var output = data.Skip(sizeof(byte) * 16).ToArray();
        var iv = data.Take(sizeof(byte) * 16).ToArray();

        using (var inputStream = new MemoryStream(output))
        using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
        using (var decryptStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
        using (var outputStream = new MemoryStream())
        {
            decryptStream.CopyTo(outputStream);
            output = outputStream.ToArray();
        }

        return output;
    }

    public static IEnumerable<byte> Encrypt(this IEnumerable<byte> data)
    {
        byte[] output = data.ToArray();
        aes.GenerateIV();
        using (var encrypter = aes.CreateEncryptor(aes.Key, aes.IV))
        using (var encryptedStream = new MemoryStream())
        using (var cryptoStream = new CryptoStream(encryptedStream, encrypter, CryptoStreamMode.Write))
        {
            cryptoStream.Write(output, 0, output.Length);
            cryptoStream.Close();
            output = encryptedStream.ToArray();
            output = aes.IV.Concat(output).ToArray();
        }

        return output;
    }

    public static IEnumerable<byte> PrependLength(this IEnumerable<byte> data)
    {
        return BitConverter.GetBytes(data.Count()).Concat(data);
    }
}