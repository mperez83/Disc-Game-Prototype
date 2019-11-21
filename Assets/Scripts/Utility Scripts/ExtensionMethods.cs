using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Creates color with corrected brightness.
    /// </summary>
    /// <param name="color">Color to correct.</param>
    /// <param name="correctionFactor">The brightness correction factor. Must be greater than 0.
    /// Values less than 1 produce darker resuls, while values greater than 1 produce brighter results.</param>
    /// <returns>
    /// Corrected <see cref="Color"/> structure.
    /// </returns>
    public static void ModifyBrightness(this SpriteRenderer self, float correctionFactor)
    {
        self.color = new Color(self.color.r * correctionFactor, self.color.g * correctionFactor, self.color.b * correctionFactor, self.color.a);
    }

    public static Color GetModifiedBrightness(this SpriteRenderer self, float correctionFactor)
    {
        return new Color(self.color.r * correctionFactor, self.color.g * correctionFactor, self.color.b * correctionFactor, self.color.a);
    }
}