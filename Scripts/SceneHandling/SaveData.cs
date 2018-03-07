using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for saving pictures
[CreateAssetMenu]
public class SaveData : MonoBehaviour {

    [SerializeField]
    public class KeyValuePairList<T>
    {
        public List<string> keys = new List<string>();
        public List<T> values = new List<T>();


        public void Clear ()
        {
            keys.Clear();
            values.Clear();
        }


        public void TrySetValue (string key, T value)
        {
            //lambda function
            //try to find the index for the key
            int index = keys.FindIndex(x => x == key);

            //if exists set it
            if(index > -1)
            {
                values[index] = value;
            }
            else
            {
                //if not make it
                keys.Add(key);
                values.Add(value);
            }
        }

        public bool TryGetValue (string key, ref T value)
        {
            int index = keys.FindIndex(x => x == key);

            if (index > -1)
            {
                value = values[index];
                return true;
            }

            return false;
        }
    }

    //write the rest of the funtions based on the
    //photo type
}
