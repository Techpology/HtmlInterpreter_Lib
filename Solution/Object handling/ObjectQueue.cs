using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace htmlInterpreter.Components
{
    //Id: 60
    //*2 https://drive.google.com/file/d/1iDDpEIJBPt0Da-TqN9ydf3dytj86inI8/view?usp=sharing
    [Obsolete("Depricated, use ObjQ")]
    public class ObjectQueue
    {
        public List<Masterpage> MasterpageQueue; //The only reason it's public is to use it as an argument when clearing queue
        public List<Masterpage> MasterpagePreviewQueue; //The only reason it's public is to use it as an argument when clearing queue

        //Preview queue goes straight threw the (Write) process.
        //But what ever is added to the preview is also added to the standard queue but not viceversa,
        //and later when trigger is detected (ctrl+s), the standard queue is saved to correct path structure.
        //So the the preview functions act just as a middle man to collect data and store it, but the standard queue
        //is what actually gets saved as the final file.
        /// <summary>
        /// Acts as a binding between all objects with data storing needed.
        /// <para>
        /// In general, you would want to use that after a certain process had been triggered which in return needed data storing
        /// </para>
        /// </summary>
        public ObjectQueue()
        {
            MasterpageQueue = new List<Masterpage>();
            MasterpagePreviewQueue = new List<Masterpage>();
        }

        /// <summary>
        /// Choose type of object to add to queue, later you can use the save queue function to store
        /// the requested data.
        /// </summary>
        /// <param name="type">Masterpage, Webpage, </param> //fill this with all possible types.
        public void AddToQueue(string type, Masterpage masterpage = null)
        {
            switch (type)
            {
                case "Masterpage":
                    MasterpageQueue.Add(masterpage);
                    break;
            }
        }

        /// <summary>
        /// Choose type of object to add to preview queue, later you can use the save queue function to store
        /// the requested data.
        /// <para>You can also use the update preview to update preview file directly instead of updating its meta temp file.</para>
        /// </summary>
        /// <param name="type">Masterpage, Webpage, </param> //fill this with all possible types.
        public void AddToPreviewQueue(string type, Masterpage masterpage = null)
        {
            switch (type)
            {
                case "Masterpage":
                    MasterpagePreviewQueue.Add(masterpage);
                    break;
            }
        }

        /// <summary>
        /// Takes in a list as an argument and clears it inside of this main scripts.
        /// <para>Used to clear queues simpler and better.</para>
        /// </summary>
        /// <param name="listObj">A object which is part of the ObjectQueue being used. (ObjectQueue.list)</param>
        public void ClearQueue(List<Masterpage> listObj)
        {
            listObj.Clear();
        }

        /// <summary>
        /// Takes all standard queues and assigns them to the right slot in the save method.
        /// </summary>
        /*public void WriteToSolution(string path)
        {
            //Write to save
            Save save = new Save(path);

            save.Save_Masterpage(MasterpageQueue);
        }

        public void WritePreviewToSolution(string path)
        {
            Save save = new Save(path);

            save.Save_PreviewMasterPage(MasterpagePreviewQueue);
        }*/

    }
}
