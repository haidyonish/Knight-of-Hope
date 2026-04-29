using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class NicknameValidator
{
    private const int MaxLength = 16;

    private static readonly Regex AllowedRegex = new Regex("^[a-zA-Z0-9]+$");

    private static readonly string[] bannedWords =
    {
        "nigger", "nigga", "n1gga", "nigg3r",
        "niger", "niga", "n1ga", "nig3r",
        "faggot", "fag", "f4gg0t",
        "kike", "spic", "chink", "gook", "wetback",
        "hitler", "nazi", "fascist", "kkk", "whitepower",
        "holocaust", "jihad", "allahakbar",

        "cunt", "rape", "rapist", "pedo", "pedophile", "ped0",
        "incest", "tranny", "transphobic",

        "suicide", "selfharm", "cutting",
        "terrorist", "isis", "bomb", "explode",

        "retard", "retarded", "mongoloid", "downsyndrome", "autistic",

        "pidor", "pidoras", "pidr", "pedik", "pedrik",
        "blyat", "blyad", "shlyukha", "shalava",
        "zaebal", "doibal", "mudak", "hui", "huy",

        "motherfucker", "muthafucka", "sonofabitch",
        "whore", "slut", "bastard"
    };

    public static bool TryValidate(string input, out string error, out string cleanName)
    {
        cleanName = input.Trim();
        error = null;

        if (string.IsNullOrEmpty(cleanName))
        {
            error = "Имя не может быть пустым";
            return false;
        }

        if (cleanName.Length > MaxLength)
        {
            error = $"Максимум {MaxLength} символов";
            return false;
        }

        if (!AllowedRegex.IsMatch(cleanName))
        {
            error = "Только латинские буквы и цифры";
            return false;
        }

        string lower = cleanName.ToLower();

        foreach (var word in bannedWords)
        {
            if (lower.Contains(word))
            {
                error = "Недопустимое слово";
                return false;
            }
        }

        return true;
    }
}