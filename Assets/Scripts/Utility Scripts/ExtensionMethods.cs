using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Modifies an existing color by the given correction factor.
    /// </summary>
    /// <param name="correctionFactor">The brightness correction factor. Must be greater than 0.
    /// Values less than 1 produce darker resuls, while values greater than 1 produce brighter results.</param>
    /// <returns>
    /// Nothing lol
    /// </returns>
    public static void ModifyBrightness(this SpriteRenderer self, float correctionFactor)
    {
        self.color = new Color(self.color.r * correctionFactor, self.color.g * correctionFactor, self.color.b * correctionFactor, self.color.a);
    }

    /// <summary>
    /// Returns a color with the corrected brightness.
    /// </summary>
    /// <param name="correctionFactor">The brightness correction factor. Must be greater than 0.
    /// Values less than 1 produce darker resuls, while values greater than 1 produce brighter results.</param>
    /// <returns>
    /// Corrected <see cref="Color"/> structure.
    /// </returns>
    public static Color GetModifiedBrightness(this SpriteRenderer self, float correctionFactor)
    {
        return new Color(self.color.r * correctionFactor, self.color.g * correctionFactor, self.color.b * correctionFactor, self.color.a);
    }

    public static Color GetModifiedBrightness(this Color self, float correctionFactor)
    {
        return new Color(self.r * correctionFactor, self.g * correctionFactor, self.b * correctionFactor, self.a);
    }



    /// <summary>
    /// Applies a force to a Rigidbody2D from the perspective of an explosion.
    /// </summary>
    /// <param name="force">Strength of the force to apply</param>
    /// <param name="explosionPosition">Position of the explosion</param>
    /// <param name="explosionRadius">Radius of the explosion. Must be larger than the distance between the two positions!</param>
    /// <remarks>
    /// This method requires very robust code for determining if the explosion is close enough to the entity to apply the force. Physics2D.OverlapCircleAll can cause
    /// issues. I initially used it to determine if a Rigidbody2D was within the influence of the explosion, but this was bad because I didn't account for the
    /// radius of the Rigidbody2D causing the object to be flagged as within the explosion, but the distance between the transforms of the explosion and the object
    /// were larger than the explosionRadius.
    /// </remarks>
    /// <returns>
    /// Nothing lol
    /// </returns>
    public static void AddExplosionForce(this Rigidbody2D self, float force, Vector2 explosionPosition, float explosionRadius)
    {
        Vector2 forceDirection = new Vector2(self.transform.position.x, self.transform.position.y) - explosionPosition;
        float distance = Vector2.Distance(self.transform.position, explosionPosition);
        force *= (explosionRadius - distance) / explosionRadius;
        self.AddForce(forceDirection.normalized * force);
    }



    public static void PlayRandomize(this AudioSource self)
    {
        PlayRandomize(self, 0.15f);
    }

    public static void PlayRandomize(this AudioSource self, float randomOffset)
    {
        self.pitch = 1 + Random.Range(-randomOffset, randomOffset);
        self.Play();
    }
}