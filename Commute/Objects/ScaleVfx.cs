namespace Commute.Objects
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// VFX for when a fish scales up.
    /// </summary>
    internal class ScaleVfx
    {
        /// <summary>
        /// The maximum number of particles.
        /// </summary>
        private const int maxParticles = 50;

        /// <summary>
        /// A list of particles.
        /// </summary>
        private readonly List<Particle> particles;

        /// <summary>
        /// Create scale VFX.
        /// </summary>
        public ScaleVfx()
        {
            particles = new List<Particle>();
        }

        /// <summary>
        /// Start the VFX from the given position.
        /// </summary>
        /// <param name="position">The position.</param>
        public void Start(Vector2 position)
        {
            // Until hitting the maximum number of particles
            for (int i=0; i<maxParticles; i++)
            {
                Random random = new Random();

                // Choose a random direction
                Vector2 direction = new Vector2(random.Next(-1, 2), random.Next(-1, 2));

                // Choose a random offset from the position
                Vector2 offset = new Vector2(random.Next(-10, 10), random.Next(-10, 10));

                // Create a new particle and add it to the list
                particles.Add(new Particle(position + offset, random.Next(3, 6), direction, random.Next(1, 4), Color.Gold));
            }
        }

        /// <summary>
        /// Update the VFX.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            List<Particle> particlesToRemove = new List<Particle>();

            // Go through each particle
            foreach (Particle particle in particles)
            {
                // Update the particle
                particle.Update(gameTime);

                // If the particles is dead
                if (particle.Dead)
                {
                    // Add it to the list of particles to remove
                    particlesToRemove.Add(particle);
                }
            }

            // Go through the list of particles to remove
            foreach (Particle particle in particlesToRemove)
            {
                // Remove the particle from the main list
                particles.Remove(particle);
            }
        }

        /// <summary>
        /// Draw the VFX.
        /// </summary>
        public void Draw()
        {
            particles.ForEach(p => p.Draw());
        }
    }
}
