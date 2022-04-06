using Donnees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContractPersistance
{
    [DataContract]
    class EnsembleAudioDTO
    {
        [DataMember (EmitDefaultValue = false)]
        public string Titre { get; set; }

        [DataMember]
        public DateTime DateAjout { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public int Note { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public string Description { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string CheminImage { get; set; }


        [DataMember]
        public int CmptEcoute { get; set; }



        [DataMember]
        public bool Favori { get; set; }

  
        [DataMember(EmitDefaultValue = false)]
        public EGenre Genre { get; set; }
    }
    static class EnsembleAudioExtension
    {
        public static EnsembleAudio ToPOCO(this EnsembleAudioDTO dto) => new EnsembleAudio(dto.Titre, dto.Description, dto.CheminImage, dto.Genre,dto.Note);
        
     
    
    
    
    }




}
