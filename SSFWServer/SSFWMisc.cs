﻿using System.Security.Cryptography;
using System.Text;

namespace SSFWServer
{
    public class SSFWMisc
    {
        public static string LayoutTemplate = "[{\"00000000-00000000-00000000-00000004\":{\"version\":3,\"wallpaper\":2,\"furniture\":" +
            "[{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000010\",\"instanceId\":\"4874595585\",\"itemId\":0,\"" +
            "positionX\":-4.287144660949707,\"positionY\":2.9999580383300781,\"positionZ\":-2.3795166015625,\"rotationX\":2.6903744583" +
            "250955E-06,\"rotationY\":0.70767402648925781,\"rotationZ\":-2.1571504476014525E-06,\"rotationW\":0.70653915405273438,\"ti" +
            "me\":1686384673},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000002\",\"instanceId\":\"4874595586\"" +
            ",\"itemId\":1,\"positionX\":-3.7360246181488037,\"positionY\":2.9999902248382568,\"positionZ\":-0.93418246507644653,\"rot" +
            "ationX\":1.5251726836140733E-05,\"rotationY\":0.92014747858047485,\"rotationZ\":-0.00032892703893594444,\"rotationW\":0.3" +
            "9157184958457947,\"time\":1686384699},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000002\",\"instan" +
            "ceId\":\"4874595587\",\"itemId\":2,\"positionX\":-4.2762022018432617,\"positionY\":2.9999568462371826,\"positionZ\":-4.15" +
            "23990631103516,\"rotationX\":1.4554960570123399E-09,\"rotationY\":0.4747755229473114,\"rotationZ\":-1.4769816480963982E-0" +
            "8,\"rotationW\":0.88010692596435547,\"time\":1686384723},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-" +
            "00000002\",\"instanceId\":\"4874595588\",\"itemId\":3,\"positionX\":-2.8646721839904785,\"positionY\":2.9999570846557617," +
            "\"positionZ\":-3.0560495853424072,\"rotationX\":0.00010053320875158533,\"rotationY\":-0.26336261630058289,\"rotationZ\":-" +
            "3.8589099858654663E-05,\"rotationW\":0.96469688415527344,\"time\":1686384751},{\"flags\":0,\"furnitureObjectId\":\"000000" +
            "00-00000000-00000002-00000001\",\"instanceId\":\"4874595589\",\"itemId\":4,\"positionX\":3.9096813201904297,\"positionY\"" +
            ":2.9995136260986328,\"positionZ\":-4.2813630104064941,\"rotationX\":4.3287433072691783E-05,\"rotationY\":-0.5309971570968" +
            "6279,\"rotationZ\":-3.9187150832731277E-05,\"rotationW\":0.8473736047744751,\"time\":1686384774},{\"flags\":0,\"furniture" +
            "ObjectId\":\"00000000-00000000-00000002-00000004\",\"instanceId\":\"4874595590\",\"itemId\":5,\"positionX\":1.84187448024" +
            "74976,\"positionY\":3.0001647472381592,\"positionZ\":-3.2746503353118896,\"rotationX\":-5.4990476201055571E-05,\"rotation" +
            "Y\":-0.53177982568740845,\"rotationZ\":-1.335094293608563E-05,\"rotationW\":0.84688264131546021,\"time\":1686384795},{\"f" +
            "lags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000008\",\"instanceId\":\"4874595591\",\"itemId\":6,\"posit" +
            "ionX\":3.4726400375366211,\"positionY\":3.0000433921813965,\"positionZ\":4.783566951751709,\"rotationX\":6.13473239354789" +
            "26E-05,\"rotationY\":0.99999260902404785,\"rotationZ\":-1.7070769899873994E-05,\"rotationW\":0.0038405421655625105,\"time" +
            "\":1686384822},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000008\",\"instanceId\":\"4874595592\"," +
            "\"itemId\":7,\"positionX\":3.4952659606933594,\"positionY\":3.0000007152557373,\"positionZ\":0.2776024341583252,\"rotatio" +
            "nX\":-1.2929040167364292E-05,\"rotationY\":-0.0061355167999863625,\"rotationZ\":-4.4378830352798104E-05,\"rotationW\":0.9" +
            "99981164932251,\"time\":1686384834},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000001\",\"instance" +
            "Id\":\"4874595593\",\"itemId\":8,\"positionX\":1.3067165613174438,\"positionY\":2.9994897842407227,\"positionZ\":2.546649" +
            "694442749,\"rotationX\":2.8451957405195571E-05,\"rotationY\":0.70562022924423218,\"rotationZ\":-8.0827621786738746E-06,\"" +
            "rotationW\":0.70859026908874512,\"time\":1686384862},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-0000" +
            "0003\",\"instanceId\":\"4874595594\",\"itemId\":9,\"positionX\":3.4803681373596191,\"positionY\":2.9999568462371826,\"pos" +
            "itionZ\":2.5385856628417969,\"rotationX\":3.1659130428352E-08,\"rotationY\":-0.70712763071060181,\"rotationZ\":8.14428204" +
            "87609424E-08,\"rotationW\":0.70708584785461426,\"time\":1686384884},{\"flags\":0,\"furnitureObjectId\":\"00000000-0000000" +
            "0-00000002-00000009\",\"instanceId\":\"4874595595\",\"itemId\":10,\"positionX\":-3.5043892860412598,\"positionY\":2.99995" +
            "68462371826,\"positionZ\":-9.527653694152832,\"rotationX\":-1.7184934222314041E-06,\"rotationY\":0.00023035785125102848,\"" +
            "rotationZ\":2.5227839728358958E-07,\"rotationW\":0.99999994039535522,\"time\":1686384912},{\"flags\":0,\"furnitureObjectI" +
            "d\":\"00000000-00000000-00000002-00000009\",\"instanceId\":\"4874595596\",\"itemId\":11,\"positionX\":3.6248698234558105," +
            "\"positionY\":2.9999566078186035,\"positionZ\":-9.5347089767456055,\"rotationX\":-2.1324558474589139E-07,\"rotationY\":2." +
            "0361580027383752E-05,\"rotationZ\":-4.7822368287597783E-08,\"rotationW\":1,\"time\":1686384931},{\"flags\":0,\"furnitureO" +
            "bjectId\":\"00000000-00000000-00000002-00000005\",\"instanceId\":\"4874595597\",\"itemId\":12,\"positionX\":-3.5068926811" +
            "218262,\"positionY\":3.4883472919464111,\"positionZ\":-9.5313901901245117,\"rotationX\":-0.00091801158851012588,\"rotatio" +
            "nY\":0.006055513396859169,\"rotationZ\":0.000585820700507611,\"rotationW\":0.99998104572296143,\"time\":1686384961,\"phot" +
            "o\":\"/Furniture/Modern2/lampOutputcube.dds\"},{\"flags\":0,\"furnitureObjectId\":\"00000000-00000000-00000002-00000005\"" +
            ",\"instanceId\":\"4874595598\",\"itemId\":13,\"positionX\":3.6171293258666992,\"positionY\":3.4891724586486816,\"position" +
            "Z\":-9.53490161895752,\"rotationX\":0.00042979296995326877,\"rotationY\":-0.0092521701008081436,\"rotationZ\":-0.00027207" +
            "753737457097,\"rotationW\":0.99995702505111694,\"time\":1686385008,\"photo\":\"/Furniture/Modern2/lampOutputcube.dds\"}]}}]";
    }

    public class SSFWUserData
    {
        public string? Username { get; set; }
        public int LogonCount { get; set; }
        public int IGA { get; set; }
    }
}
