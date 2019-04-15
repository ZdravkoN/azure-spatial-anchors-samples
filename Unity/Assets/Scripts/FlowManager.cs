using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharingService
{
    public class FlowManager : MonoBehaviour
    {
        public ScanningScreen ScanningScreen;
        public AnchorActionSelect AnchorActionSelectScreen;
        public CreateAnchorScreen AnchorCreationScreen;

        // Start is called before the first frame update
        void Start()
        {
            var screens = GetComponentsInChildren<BaseScreen>();
            for (var idx = 0; idx < screens.Length; idx++)
            {
                screens[idx].Hide();
            }

            StartCoroutine(Flow());
        }

        IEnumerator Flow()
        {
            yield return new WaitForSeconds(1.0f);

            ScanningScreen.Show();

            yield return new WaitForSeconds(5.0f);
            yield return new WaitUntil(() => ScanningScreen.IsDone);

            ScanningScreen.Hide();

            AnchorActionSelectScreen.Show();

            yield return new WaitUntil(() => AnchorActionSelectScreen.IsDone);

            AnchorActionSelectScreen.Hide();

            AnchorCreationScreen.Show();

            yield return new WaitUntil(() => AnchorCreationScreen.IsDone);

            AnchorCreationScreen.Hide();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
