using FreeAdobe.src.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAdobe.src
{
    public class AdobePatchUtil
    {
        //2019\2020\2021系列通用特征码
        private static string targetByteStr= "BB 02 00 00 00 8B C3 87 05";
        private static string newByteStr = "BB 03 00 00 00 8B C3 87 05";

        private static string acrobatTargetByte = "33 C0 40 89 85 ?? ?? ?? ?? 8D 85";
        private static string acrobatNewByte = "40 40 40 89 85 ?? ?? ?? ?? 8D 85";
        private static List<PatchInfo> patchInfos;

        public static List<PatchInfo> loadAllProduct() {
            if (patchInfos == null)
            {
                patchInfos = new List<PatchInfo>();
                initPatchInfo("2021");
                initPatchInfo("2020");
                initPatchInfo("2019");
            }
            return patchInfos;
        }


        public static List<PatchInfo> loadProductPatchInfo(AdobeProduct productName,string version) {
            if (patchInfos == null) {
                patchInfos = new List<PatchInfo>();
                initPatchInfo("2021");
                initPatchInfo("2020");
                initPatchInfo("2019");
            }
            List<PatchInfo> productPatchInfo = new List<PatchInfo>();
            foreach(PatchInfo patchInfo in patchInfos) {
                if (patchInfo.Product == productName&&patchInfo.Version.Equals(version)) {
                    productPatchInfo.Add(patchInfo);
                }
            }
            return productPatchInfo;
        }

        public static void initPatchInfo(string version) {
            
            patchInfos.Add(new PatchInfo(AdobeProduct.AfterEffects, "AfterEffects", version, "C:/Program Files/Adobe/Adobe After Effects "+version+"/Support Files/AfterFXLib.dll", "C:/Program Files/Adobe/Adobe After Effects " + version + "/Support Files/AfterFX.exe", "AfterFXLib.dll", targetByteStr,newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Animate, "Animate",version, "C:/Program Files/Adobe/Adobe Animate " + version + "/Animate.exe", "C:/Program Files/Adobe/Adobe Animate " + version + "/Animate.exe", "Animate.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Audition, "Audition", version, "C:/Program Files/Adobe/Adobe Audition " + version + "/AuUI.dll", "C:/Program Files/Adobe/Adobe Audition " + version + "/Adobe Audition.exe", "AuUI.dll", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Bridge, "Bridge", version, "C:/Program Files/Adobe/Adobe Bridge " + version + "/Bridge.exe", "C:/Program Files/Adobe/Adobe Bridge " + version + "/Bridge.exe", "Bridge.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.CharacterAnimator, "CharacterAnimator", version, "C:/Program Files/Adobe/Adobe Character Animator " + version + "/Support Files/Character Animator.exe", "C:/Program Files/Adobe/Adobe Character Animator " + version + "/Support Files/Character Animator.exe", "Character Animator.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Dreamweaver, "Dreamweaver", version, "C:/Program Files/Adobe/Adobe Dreamweaver " + version + "/Dreamweaver.exe", "C:/Program Files/Adobe/Adobe Dreamweaver " + version + "/Dreamweaver.exe", "Dreamweaver.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Illustrator, "Illustrator", version, "C:/Program Files/Adobe/Adobe Illustrator " + version + "/Support Files/Contents/Windows/Illustrator.exe", "C:/Program Files/Adobe/Adobe Illustrator " + version + "/Support Files/Contents/Windows/Illustrator.exe", "Illustrator.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.LightroomClassic, "LightroomClassic", version, "C:/Program Files/Adobe/Adobe Lightroom Classic/Lightroom.exe", "C:/Program Files/Adobe/Adobe Lightroom Classic/Lightroom.exe", "Lightroom.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.MediaEncoder, "MediaEncoder", version, "C:/Program Files/Adobe Media Encoder " + version + "/Adobe Media Encoder.exe", "C:/Program Files/Adobe Media Encoder " + version + "/Adobe Media Encoder.exe", "Character Animator.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Photoshop, "Photoshop", version, "C:/Program Files/Adobe/Adobe Photoshop " + version + "/Photoshop.exe", "C:/Program Files/Adobe/Adobe Photoshop " + version + "/Photoshop.exe", "Photoshop.exe", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.Prelude, "Prelude", version, "C:/Program Files/Adobe/Adobe Prelude " + version + "/Registration.dll", "C:/Program Files/Adobe/Adobe Prelude " + version + "/Adobe Prelude.exe", "Registration.dll", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.PremierePro, "PremierePro", version, "C:/Program Files/Adobe/Adobe Premiere Pro " + version + "/Registration.dll", "C:/Program Files/Adobe/Adobe Premiere Pro " + version + "/Adobe Premiere Pro.exe", "Registration.dll", targetByteStr, newByteStr));

            patchInfos.Add(new PatchInfo(AdobeProduct.InCopy, "InCopy", version, "C:/Program Files/Adobe/Adobe InCopy "+version+"/Public.dll", "C:/Program Files/Adobe/Adobe InCopy " + version + "/InCopy.exe", "Public.dll", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.InDesign, "InDesign", version, "C:/Program Files/Adobe/Adobe InDesign " + version + "/Public.dll", "C:/Program Files/Adobe/Adobe InDesign " + version + "/InDesign.exe", "Public.dll", targetByteStr, newByteStr));
            patchInfos.Add(new PatchInfo(AdobeProduct.AcrobatDC, "AcrobatDC", version, "C:/Program Files (x86)/Adobe/Acrobat DC/Acrobat/Acrobat.dll", "C:/Program Files (x86)/Adobe/Acrobat DC/Acrobat/Acrobat.exe", "Acrobat.dll", acrobatTargetByte, acrobatNewByte));
        }

        public static bool patchProduct(AdobeProduct product, string version) {
            List<PatchInfo> productPatchInfo = loadProductPatchInfo(product,version);
            bool succ = true ;
            foreach (PatchInfo info in productPatchInfo) {
                //succ&=ByteHelper.replaceByte(info.InstallPath,info.TargetByteStr,info.NewByteStr);
                succ &= StrongByteHelper.replaceByte(info.InstallPath, info.TargetByteStr, info.NewByteStr);
            }
            return succ;
        }
    }
}
