using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformMovement : MonoBehaviour
{
    public float direction { get; private set; }
    public Vector3 velocity { get; private set; }
    public Material disabledMaterial;
    public Material leftMaterial;
    public Material rightMaterial;
    public bool isEnabled;
    public float speed;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.rotation.y < 0 ? -1 : 1;
        velocity = new Vector3(direction, 0, 0) * speed;

        meshRenderer = GetComponent<MeshRenderer>();
        SetMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        if (ControlManager.IsTaped() && isEnabled)
        {
            if (!ControlManager.IsTouchingGameObject(gameObject))
                return;

            direction *= -1;
            velocity *= -1;
            transform.rotation *= Quaternion.Euler(new Vector3(0, -180, 0));

            SetMaterial();
        }
    }

    private void SetMaterial()
    {
        meshRenderer.material = isEnabled ? (direction < 0 ? leftMaterial : rightMaterial) : disabledMaterial;

    }
}
