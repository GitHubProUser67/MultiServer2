﻿namespace BackendProject.WebAPIs.PREMIUMAGENCY
{
    public class PREMIUMAGENCYClass : IDisposable
    {
        string workpath;
        string absolutepath;
        string method;
        private bool disposedValue;

        public PREMIUMAGENCYClass(string method, string absolutepath, string workpath)
        {
            this.workpath = workpath;
            this.absolutepath = absolutepath;
            this.method = method;
        }

        public string? ProcessRequest(byte[]? PostData, string? ContentType)
        {
            if (string.IsNullOrEmpty(absolutepath))
                return null;

            switch (method)
            {
                case "POST":
                    switch (absolutepath)
                    {
                        case "/eventController/checkEvent.do":
                            return Event.checkEventRequestPOST(PostData, ContentType);
                        case "/eventController/entryEvent.do":
                            return Event.entryEventRequestPOST(PostData, ContentType);
                        case "/eventController/getUserEventCustom.do":
                            return Event.getUserEventCustomRequestPOST(PostData, ContentType, workpath);
                        case "/eventController/clearEvent.do":
                            return Event.clearEventRequestPOST(PostData, ContentType);
                        case "/eventController/getEventTrigger.do":
                            return Trigger.getEventTriggerRequestPOST(PostData, ContentType, workpath);
                        case "/eventController/confirmEventTrigger.do":
                            return Trigger.confirmEventTriggerRequestPOST(PostData, ContentType, workpath);
                        case "/eventController/getResource.do":
                            return Resource.getResourcePOST(PostData, ContentType, workpath);
                        case "/eventController/setUserEventCustom.do":
                            return Custom.setUserEventCustomPOST(PostData, ContentType, workpath);
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    workpath = string.Empty;
                    absolutepath = string.Empty;
                    method = string.Empty;
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~PREMIUMAGENCYClass()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
