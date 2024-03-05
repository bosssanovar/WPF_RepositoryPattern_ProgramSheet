using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public class SpeakerOnOffEntity
    {

        public List<SpeakerOnOffDetailEntity> Details { get; } = [];

        public void InitSpeakerCount(int count)
        {
            Details.Clear();
            for (int i = 0; i < count; i++)
            {
                var speakerOnOffDetail = new SpeakerOnOffDetailEntity();
                speakerOnOffDetail.Init(count);
                Details.Add(speakerOnOffDetail);
            }
        }
    }
}
