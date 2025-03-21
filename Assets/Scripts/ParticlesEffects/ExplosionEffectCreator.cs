using UnityEngine;

public class ExplosionEffectCreator : MonoBehaviour
{
    [SerializeField] private Material additiveMaterial; // Assign a material that uses the "Particles/Additive" shader.
    [SerializeField] private Texture particleTexture; // Optionally assign a custom texture for the particles.

    public static void CreateExplosion(Vector3 pos, Material additiveMat, Texture particleTex)
    {
        Vector3 explosionPos = new Vector3(pos.x, pos.y, -2f);
        GameObject explosionGO = new GameObject("ExplosionEffect");
        explosionGO.transform.position = explosionPos;

        ParticleSystem ps = explosionGO.AddComponent<ParticleSystem>();
        ParticleSystemRenderer psRenderer = explosionGO.GetComponent<ParticleSystemRenderer>();

        if (additiveMat != null)
        {
            if (particleTex != null)
            {
                additiveMat.mainTexture = particleTex;
            }
            psRenderer.material = additiveMat;
        }

        // Ensure the explosion appears on top
        psRenderer.sortingOrder = 100;

        var main = ps.main;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.duration = 1f;
        main.loop = false;
        main.startLifetime = 1f;
        main.startSpeed = 5f;
        main.startSize = 0.5f;
        main.startColor = Color.white;
        main.playOnAwake = false;

        var emission = ps.emission;
        emission.rateOverTime = 0f;
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, 50);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.5f;

        ps.Play();
        Destroy(explosionGO, main.duration + main.startLifetime.constant);
    }
}
