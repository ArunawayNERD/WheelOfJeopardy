using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.UI
{

    public class UseToken : MonoBehaviour
    {
        public GameEngine gameEngine;

        // Start is called before the first frame update
        void Start()
        {
            // TODO Implement in Target.
        }

        // Update is called once per frame
        void Update()
        {
            // TODO Implement in Target.
        }

        public void Display(bool disp, string invoker, int qPts)
        {
            // Disclaimer: I don't know how this will actually be implemented.
            // For now, a call to this triggers the use of a token (forgoing choice for Minimal).
            // TODO: Make this method trigger a button display whose call back will call gameEngine.tokenUsed()
        //    if (disp == true)
        //    {
        //        gameEngine.tokenUsed(true, invoker, qPts);
        //    }
        }
    }
}
