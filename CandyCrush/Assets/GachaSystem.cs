using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class GachaSystem : MonoBehaviour
{
    [Header("Protein Powder")]
    [SerializeField] int summonCost;

    bool canPull;

    [SerializeField] Animator cap;
    [SerializeField] Animator bottle;
    [SerializeField] VisualEffect visualEffect;

    [SerializeField] TextMeshProUGUI pullCurrencyText;
    [SerializeField] TextMeshProUGUI homeCurrencyText;

    StorageSystem storageSystem;

    [SerializeField] public GameObject[] displaySlots;

    private void Start()
    {
        canPull = true;
        storageSystem = GameObject.FindWithTag("StorageSystem").GetComponent<StorageSystem>();
    }

    void Update()
    {
        if(pullCurrencyText)
            pullCurrencyText.text = "PROTEIN POWDER: " + storageSystem.proteinCount;

        if (homeCurrencyText)
            homeCurrencyText.text = storageSystem.proteinCount.ToString();
    }

    public void ObtainNewCharacter()
    {
        if (!canPull || storageSystem.proteinCount <= summonCost)
            return;

        canPull = false;

        storageSystem.ChangeProteinCount(-300);

        cap.SetBool("open", true);
        bottle.SetBool("open", true);
        visualEffect.Play();

        AddCharacter(Random.Range(0, storageSystem.characters.Length));

        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5f);
        cap.SetBool("open", false);
        bottle.SetBool("open", false);
        canPull = true;
    }

    void AddCharacter(int index)
    {
        StorageSystem storageSystem = GameObject.FindWithTag("StorageSystem").GetComponent<StorageSystem>();
        if (storageSystem)
            storageSystem.AddCharacter(index);
    }
}
