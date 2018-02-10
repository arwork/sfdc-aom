using System;

namespace CustomLogic.Services.AomMetaService
{
public class AomMetaViewModel 
{

    public AomMetaViewModel()
    {
        ID = Guid.NewGuid();
    }

public Guid ID {get;set;}
public string Name {get;set;}
}
}



