using System;

public class VigenereCipher
{
    const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    readonly string letters;

    public VigenereCipher(string alphabet = null)
    {
        letters = string.IsNullOrEmpty(alphabet) ? Alphabet : alphabet;
    }

    //генерация повтора ключа
    private string GetRepeatKey(string s, int n)
    {
        var p = s;
        while (p.Length < n)
        {
            p += p;
        }

        return p.Substring(0, n);
    }

    private string Vigenere(string source, string key, bool encrypting = true)
    {
        var gamma = GetRepeatKey(key, source.Length);
        var retValue = "";
        var N = letters.Length;

        for (int i = 0; i < source.Length; i++)
        {
            var SourceletterIndex = letters.IndexOf(source[i]);
            var KeyIndex = letters.IndexOf(gamma[i]);
            if (SourceletterIndex < 0)
            {
                //если буква не найдена, добавляем её в исходном виде
                retValue += source[i].ToString();
            }
            else
            {
                retValue += letters[(N + SourceletterIndex + ((encrypting ? 1 : -1) * KeyIndex)) % N].ToString();
            }
        }

        return retValue;
    }

    //шифрование текста
    public string Encrypt(string source, string key)
        => Vigenere(source, key);

    //дешифрование текста
    public string Decrypt(string encrypted, string key)
        => Vigenere(encrypted, key, false);
}

class Task1
{
    static void Main(string[] args)
    {
        var cipher = new VigenereCipher();
        Console.Write("Введите текст: ");
        var inputText = Console.ReadLine().ToUpper();
        Console.Write("Введите ключ: ");
        var key = Console.ReadLine().ToUpper();
        var encryptedText = cipher.Encrypt(inputText, key);
        Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
        Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(encryptedText, key));
        Console.ReadLine();
    }
}