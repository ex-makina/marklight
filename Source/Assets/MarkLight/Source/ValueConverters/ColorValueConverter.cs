#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
#endregion

namespace MarkLight.ValueConverters
{
    /// <summary>
    /// Value converter for Color type.
    /// </summary>
    public class ColorValueConverter : ValueConverter
    {
        #region Fields

        public static Dictionary<string, Color> ColorCodes;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ColorValueConverter()
        {
            _type = typeof(Color);
        }

        /// <summary>
        /// Initializes a static instance of the class.
        /// </summary>
        static ColorValueConverter()
        {
            ColorCodes = new Dictionary<string, Color>();
            ColorCodes.Add("clear", Color.clear);
            ColorCodes.Add("grey", Color.grey);
            ColorCodes.Add("red", Color.red);
            ColorCodes.Add("transparent", Color.clear);

            ColorCodes.Add("aliceblue", HexToColor("#f0f8ff").Value);
            ColorCodes.Add("antiquewhite", HexToColor("#faebd7").Value);
            ColorCodes.Add("antiquewhite1", HexToColor("#ffefdb").Value);
            ColorCodes.Add("antiquewhite2", HexToColor("#eedfcc").Value);
            ColorCodes.Add("antiquewhite3", HexToColor("#cdc0b0").Value);
            ColorCodes.Add("antiquewhite4", HexToColor("#8b8378").Value);
            ColorCodes.Add("aquamarine", HexToColor("#7fffd4").Value);
            ColorCodes.Add("aquamarine1", HexToColor("#7fffd4").Value);
            ColorCodes.Add("aquamarine2", HexToColor("#76eec6").Value);
            ColorCodes.Add("aquamarine4", HexToColor("#458b74").Value);
            ColorCodes.Add("azure", HexToColor("#f0ffff").Value);
            ColorCodes.Add("azure1", HexToColor("#f0ffff").Value);
            ColorCodes.Add("azure2", HexToColor("#e0eeee").Value);
            ColorCodes.Add("azure3", HexToColor("#c1cdcd").Value);
            ColorCodes.Add("azure4", HexToColor("#838b8b").Value);
            ColorCodes.Add("beige", HexToColor("#f5f5dc").Value);
            ColorCodes.Add("bisque", HexToColor("#ffe4c4").Value);
            ColorCodes.Add("bisque1", HexToColor("#ffe4c4").Value);
            ColorCodes.Add("bisque2", HexToColor("#eed5b7").Value);
            ColorCodes.Add("bisque3", HexToColor("#cdb79e").Value);
            ColorCodes.Add("bisque4", HexToColor("#8b7d6b").Value);
            ColorCodes.Add("black", HexToColor("#000000").Value);
            ColorCodes.Add("blanchedalmond", HexToColor("#ffebcd").Value);
            ColorCodes.Add("blue", HexToColor("#0000ff").Value);
            ColorCodes.Add("blue1", HexToColor("#0000ff").Value);
            ColorCodes.Add("blue2", HexToColor("#0000ee").Value);
            ColorCodes.Add("blue4", HexToColor("#00008b").Value);
            ColorCodes.Add("blueviolet", HexToColor("#8a2be2").Value);
            ColorCodes.Add("brown", HexToColor("#a52a2a").Value);
            ColorCodes.Add("brown1", HexToColor("#ff4040").Value);
            ColorCodes.Add("brown2", HexToColor("#ee3b3b").Value);
            ColorCodes.Add("brown3", HexToColor("#cd3333").Value);
            ColorCodes.Add("brown4", HexToColor("#8b2323").Value);
            ColorCodes.Add("burlywood", HexToColor("#deb887").Value);
            ColorCodes.Add("burlywood1", HexToColor("#ffd39b").Value);
            ColorCodes.Add("burlywood2", HexToColor("#eec591").Value);
            ColorCodes.Add("burlywood3", HexToColor("#cdaa7d").Value);
            ColorCodes.Add("burlywood4", HexToColor("#8b7355").Value);
            ColorCodes.Add("cadetblue", HexToColor("#5f9ea0").Value);
            ColorCodes.Add("cadetblue1", HexToColor("#98f5ff").Value);
            ColorCodes.Add("cadetblue2", HexToColor("#8ee5ee").Value);
            ColorCodes.Add("cadetblue3", HexToColor("#7ac5cd").Value);
            ColorCodes.Add("cadetblue4", HexToColor("#53868b").Value);
            ColorCodes.Add("chartreuse", HexToColor("#7fff00").Value);
            ColorCodes.Add("chartreuse1", HexToColor("#7fff00").Value);
            ColorCodes.Add("chartreuse2", HexToColor("#76ee00").Value);
            ColorCodes.Add("chartreuse3", HexToColor("#66cd00").Value);
            ColorCodes.Add("chartreuse4", HexToColor("#458b00").Value);
            ColorCodes.Add("chocolate", HexToColor("#d2691e").Value);
            ColorCodes.Add("chocolate1", HexToColor("#ff7f24").Value);
            ColorCodes.Add("chocolate2", HexToColor("#ee7621").Value);
            ColorCodes.Add("chocolate3", HexToColor("#cd661d").Value);
            ColorCodes.Add("coral", HexToColor("#ff7f50").Value);
            ColorCodes.Add("coral1", HexToColor("#ff7256").Value);
            ColorCodes.Add("coral2", HexToColor("#ee6a50").Value);
            ColorCodes.Add("coral3", HexToColor("#cd5b45").Value);
            ColorCodes.Add("coral4", HexToColor("#8b3e2f").Value);
            ColorCodes.Add("cornflowerblue", HexToColor("#6495ed").Value);
            ColorCodes.Add("cornsilk", HexToColor("#fff8dc").Value);
            ColorCodes.Add("cornsilk1", HexToColor("#fff8dc").Value);
            ColorCodes.Add("cornsilk2", HexToColor("#eee8cd").Value);
            ColorCodes.Add("cornsilk3", HexToColor("#cdc8b1").Value);
            ColorCodes.Add("cornsilk4", HexToColor("#8b8878").Value);
            ColorCodes.Add("cyan", HexToColor("#00ffff").Value);
            ColorCodes.Add("cyan1", HexToColor("#00ffff").Value);
            ColorCodes.Add("cyan2", HexToColor("#00eeee").Value);
            ColorCodes.Add("cyan3", HexToColor("#00cdcd").Value);
            ColorCodes.Add("cyan4", HexToColor("#008b8b").Value);
            ColorCodes.Add("darkgoldenrod", HexToColor("#b8860b").Value);
            ColorCodes.Add("darkgoldenrod1", HexToColor("#ffb90f").Value);
            ColorCodes.Add("darkgoldenrod2", HexToColor("#eead0e").Value);
            ColorCodes.Add("darkgoldenrod3", HexToColor("#cd950c").Value);
            ColorCodes.Add("darkgoldenrod4", HexToColor("#8b6508").Value);
            ColorCodes.Add("darkgreen", HexToColor("#006400").Value);
            ColorCodes.Add("darkkhaki", HexToColor("#bdb76b").Value);
            ColorCodes.Add("darkolivegreen", HexToColor("#556b2f").Value);
            ColorCodes.Add("darkolivegreen1", HexToColor("#caff70").Value);
            ColorCodes.Add("darkolivegreen2", HexToColor("#bcee68").Value);
            ColorCodes.Add("darkolivegreen3", HexToColor("#a2cd5a").Value);
            ColorCodes.Add("darkolivegreen4", HexToColor("#6e8b3d").Value);
            ColorCodes.Add("darkorange", HexToColor("#ff8c00").Value);
            ColorCodes.Add("darkorange1", HexToColor("#ff7f00").Value);
            ColorCodes.Add("darkorange2", HexToColor("#ee7600").Value);
            ColorCodes.Add("darkorange3", HexToColor("#cd6600").Value);
            ColorCodes.Add("darkorange4", HexToColor("#8b4500").Value);
            ColorCodes.Add("darkorchid", HexToColor("#9932cc").Value);
            ColorCodes.Add("darkorchid1", HexToColor("#bf3eff").Value);
            ColorCodes.Add("darkorchid2", HexToColor("#b23aee").Value);
            ColorCodes.Add("darkorchid3", HexToColor("#9a32cd").Value);
            ColorCodes.Add("darkorchid4", HexToColor("#68228b").Value);
            ColorCodes.Add("darksalmon", HexToColor("#e9967a").Value);
            ColorCodes.Add("darkseagreen", HexToColor("#8fbc8f").Value);
            ColorCodes.Add("darkseagreen1", HexToColor("#c1ffc1").Value);
            ColorCodes.Add("darkseagreen2", HexToColor("#b4eeb4").Value);
            ColorCodes.Add("darkseagreen3", HexToColor("#9bcd9b").Value);
            ColorCodes.Add("darkseagreen4", HexToColor("#698b69").Value);
            ColorCodes.Add("darkslateblue", HexToColor("#483d8b").Value);
            ColorCodes.Add("darkslategray", HexToColor("#2f4f4f").Value);
            ColorCodes.Add("darkslategray1", HexToColor("#97ffff").Value);
            ColorCodes.Add("darkslategray2", HexToColor("#8deeee").Value);
            ColorCodes.Add("darkslategray3", HexToColor("#79cdcd").Value);
            ColorCodes.Add("darkslategray4", HexToColor("#528b8b").Value);
            ColorCodes.Add("darkturquoise", HexToColor("#00ced1").Value);
            ColorCodes.Add("darkviolet", HexToColor("#9400d3").Value);
            ColorCodes.Add("deeppink", HexToColor("#ff1493").Value);
            ColorCodes.Add("deepindigo", HexToColor("#8A2BE2").Value);
            ColorCodes.Add("deeppink1", HexToColor("#ff1493").Value);
            ColorCodes.Add("deeppink2", HexToColor("#ee1289").Value);
            ColorCodes.Add("deeppink3", HexToColor("#cd1076").Value);
            ColorCodes.Add("deeppink4", HexToColor("#8b0a50").Value);
            ColorCodes.Add("deepskyblue", HexToColor("#00bfff").Value);
            ColorCodes.Add("deepskyblue1", HexToColor("#00bfff").Value);
            ColorCodes.Add("deepskyblue2", HexToColor("#00b2ee").Value);
            ColorCodes.Add("deepskyblue3", HexToColor("#009acd").Value);
            ColorCodes.Add("deepskyblue4", HexToColor("#00688b").Value);
            ColorCodes.Add("denim", HexToColor("#1560BD").Value);
            ColorCodes.Add("dimgray", HexToColor("#696969").Value);
            ColorCodes.Add("dodgerblue", HexToColor("#1e90ff").Value);
            ColorCodes.Add("dodgerblue1", HexToColor("#1e90ff").Value);
            ColorCodes.Add("dodgerblue2", HexToColor("#1c86ee").Value);
            ColorCodes.Add("dodgerblue3", HexToColor("#1874cd").Value);
            ColorCodes.Add("dodgerblue4", HexToColor("#104e8b").Value);
            ColorCodes.Add("electricindigo", HexToColor("#6F00FF").Value);
            ColorCodes.Add("firebrick", HexToColor("#b22222").Value);
            ColorCodes.Add("firebrick1", HexToColor("#ff3030").Value);
            ColorCodes.Add("firebrick2", HexToColor("#ee2c2c").Value);
            ColorCodes.Add("firebrick3", HexToColor("#cd2626").Value);
            ColorCodes.Add("firebrick4", HexToColor("#8b1a1a").Value);
            ColorCodes.Add("floralwhite", HexToColor("#fffaf0").Value);
            ColorCodes.Add("forestgreen", HexToColor("#228b22").Value);
            ColorCodes.Add("gainsboro", HexToColor("#dcdcdc").Value);
            ColorCodes.Add("ghostwhite", HexToColor("#f8f8ff").Value);
            ColorCodes.Add("gold", HexToColor("#ffd700").Value);
            ColorCodes.Add("gold1", HexToColor("#ffd700").Value);
            ColorCodes.Add("gold2", HexToColor("#eec900").Value);
            ColorCodes.Add("gold3", HexToColor("#cdad00").Value);
            ColorCodes.Add("gold4", HexToColor("#8b7500").Value);
            ColorCodes.Add("goldenrod", HexToColor("#daa520").Value);
            ColorCodes.Add("goldenrod1", HexToColor("#ffc125").Value);
            ColorCodes.Add("goldenrod2", HexToColor("#eeb422").Value);
            ColorCodes.Add("goldenrod3", HexToColor("#cd9b1d").Value);
            ColorCodes.Add("goldenrod4", HexToColor("#8b6914").Value);
            ColorCodes.Add("gray", HexToColor("#bebebe").Value);
            ColorCodes.Add("gray1", HexToColor("#030303").Value);
            ColorCodes.Add("gray10", HexToColor("#1a1a1a").Value);
            ColorCodes.Add("gray11", HexToColor("#1c1c1c").Value);
            ColorCodes.Add("gray12", HexToColor("#1f1f1f").Value);
            ColorCodes.Add("gray13", HexToColor("#212121").Value);
            ColorCodes.Add("gray14", HexToColor("#242424").Value);
            ColorCodes.Add("gray15", HexToColor("#262626").Value);
            ColorCodes.Add("gray16", HexToColor("#292929").Value);
            ColorCodes.Add("gray17", HexToColor("#2b2b2b").Value);
            ColorCodes.Add("gray18", HexToColor("#2e2e2e").Value);
            ColorCodes.Add("gray19", HexToColor("#303030").Value);
            ColorCodes.Add("gray2", HexToColor("#050505").Value);
            ColorCodes.Add("gray20", HexToColor("#333333").Value);
            ColorCodes.Add("gray21", HexToColor("#363636").Value);
            ColorCodes.Add("gray22", HexToColor("#383838").Value);
            ColorCodes.Add("gray23", HexToColor("#3b3b3b").Value);
            ColorCodes.Add("gray24", HexToColor("#3d3d3d").Value);
            ColorCodes.Add("gray25", HexToColor("#404040").Value);
            ColorCodes.Add("gray26", HexToColor("#424242").Value);
            ColorCodes.Add("gray27", HexToColor("#454545").Value);
            ColorCodes.Add("gray28", HexToColor("#474747").Value);
            ColorCodes.Add("gray29", HexToColor("#4a4a4a").Value);
            ColorCodes.Add("gray3", HexToColor("#080808").Value);
            ColorCodes.Add("gray30", HexToColor("#4d4d4d").Value);
            ColorCodes.Add("gray31", HexToColor("#4f4f4f").Value);
            ColorCodes.Add("gray32", HexToColor("#525252").Value);
            ColorCodes.Add("gray33", HexToColor("#545454").Value);
            ColorCodes.Add("gray34", HexToColor("#575757").Value);
            ColorCodes.Add("gray35", HexToColor("#595959").Value);
            ColorCodes.Add("gray36", HexToColor("#5c5c5c").Value);
            ColorCodes.Add("gray37", HexToColor("#5e5e5e").Value);
            ColorCodes.Add("gray38", HexToColor("#616161").Value);
            ColorCodes.Add("gray39", HexToColor("#636363").Value);
            ColorCodes.Add("gray4", HexToColor("#0a0a0a").Value);
            ColorCodes.Add("gray40", HexToColor("#666666").Value);
            ColorCodes.Add("gray41", HexToColor("#696969").Value);
            ColorCodes.Add("gray42", HexToColor("#6b6b6b").Value);
            ColorCodes.Add("gray43", HexToColor("#6e6e6e").Value);
            ColorCodes.Add("gray44", HexToColor("#707070").Value);
            ColorCodes.Add("gray45", HexToColor("#737373").Value);
            ColorCodes.Add("gray46", HexToColor("#757575").Value);
            ColorCodes.Add("gray47", HexToColor("#787878").Value);
            ColorCodes.Add("gray48", HexToColor("#7a7a7a").Value);
            ColorCodes.Add("gray49", HexToColor("#7d7d7d").Value);
            ColorCodes.Add("gray5", HexToColor("#0d0d0d").Value);
            ColorCodes.Add("gray50", HexToColor("#7f7f7f").Value);
            ColorCodes.Add("gray51", HexToColor("#828282").Value);
            ColorCodes.Add("gray52", HexToColor("#858585").Value);
            ColorCodes.Add("gray53", HexToColor("#878787").Value);
            ColorCodes.Add("gray54", HexToColor("#8a8a8a").Value);
            ColorCodes.Add("gray55", HexToColor("#8c8c8c").Value);
            ColorCodes.Add("gray56", HexToColor("#8f8f8f").Value);
            ColorCodes.Add("gray57", HexToColor("#919191").Value);
            ColorCodes.Add("gray58", HexToColor("#949494").Value);
            ColorCodes.Add("gray59", HexToColor("#969696").Value);
            ColorCodes.Add("gray6", HexToColor("#0f0f0f").Value);
            ColorCodes.Add("gray60", HexToColor("#999999").Value);
            ColorCodes.Add("gray61", HexToColor("#9c9c9c").Value);
            ColorCodes.Add("gray62", HexToColor("#9e9e9e").Value);
            ColorCodes.Add("gray63", HexToColor("#a1a1a1").Value);
            ColorCodes.Add("gray64", HexToColor("#a3a3a3").Value);
            ColorCodes.Add("gray65", HexToColor("#a6a6a6").Value);
            ColorCodes.Add("gray66", HexToColor("#a8a8a8").Value);
            ColorCodes.Add("gray67", HexToColor("#ababab").Value);
            ColorCodes.Add("gray68", HexToColor("#adadad").Value);
            ColorCodes.Add("gray69", HexToColor("#b0b0b0").Value);
            ColorCodes.Add("gray7", HexToColor("#121212").Value);
            ColorCodes.Add("gray70", HexToColor("#b3b3b3").Value);
            ColorCodes.Add("gray71", HexToColor("#b5b5b5").Value);
            ColorCodes.Add("gray72", HexToColor("#b8b8b8").Value);
            ColorCodes.Add("gray73", HexToColor("#bababa").Value);
            ColorCodes.Add("gray74", HexToColor("#bdbdbd").Value);
            ColorCodes.Add("gray75", HexToColor("#bfbfbf").Value);
            ColorCodes.Add("gray76", HexToColor("#c2c2c2").Value);
            ColorCodes.Add("gray77", HexToColor("#c4c4c4").Value);
            ColorCodes.Add("gray78", HexToColor("#c7c7c7").Value);
            ColorCodes.Add("gray79", HexToColor("#c9c9c9").Value);
            ColorCodes.Add("gray8", HexToColor("#141414").Value);
            ColorCodes.Add("gray80", HexToColor("#cccccc").Value);
            ColorCodes.Add("gray81", HexToColor("#cfcfcf").Value);
            ColorCodes.Add("gray82", HexToColor("#d1d1d1").Value);
            ColorCodes.Add("gray83", HexToColor("#d4d4d4").Value);
            ColorCodes.Add("gray84", HexToColor("#d6d6d6").Value);
            ColorCodes.Add("gray85", HexToColor("#d9d9d9").Value);
            ColorCodes.Add("gray86", HexToColor("#dbdbdb").Value);
            ColorCodes.Add("gray87", HexToColor("#dedede").Value);
            ColorCodes.Add("gray88", HexToColor("#e0e0e0").Value);
            ColorCodes.Add("gray89", HexToColor("#e3e3e3").Value);
            ColorCodes.Add("gray9", HexToColor("#171717").Value);
            ColorCodes.Add("gray90", HexToColor("#e5e5e5").Value);
            ColorCodes.Add("gray91", HexToColor("#e8e8e8").Value);
            ColorCodes.Add("gray92", HexToColor("#ebebeb").Value);
            ColorCodes.Add("gray93", HexToColor("#ededed").Value);
            ColorCodes.Add("gray94", HexToColor("#f0f0f0").Value);
            ColorCodes.Add("gray95", HexToColor("#f2f2f2").Value);
            ColorCodes.Add("gray97", HexToColor("#f7f7f7").Value);
            ColorCodes.Add("gray98", HexToColor("#fafafa").Value);
            ColorCodes.Add("gray99", HexToColor("#fcfcfc").Value);
            ColorCodes.Add("green", HexToColor("#00ff00").Value);
            ColorCodes.Add("green1", HexToColor("#00ff00").Value);
            ColorCodes.Add("green2", HexToColor("#00ee00").Value);
            ColorCodes.Add("green3", HexToColor("#00cd00").Value);
            ColorCodes.Add("green4", HexToColor("#008b00").Value);
            ColorCodes.Add("greenyellow", HexToColor("#adff2f").Value);
            ColorCodes.Add("honeydew", HexToColor("#f0fff0").Value);
            ColorCodes.Add("honeydew1", HexToColor("#f0fff0").Value);
            ColorCodes.Add("honeydew2", HexToColor("#e0eee0").Value);
            ColorCodes.Add("honeydew3", HexToColor("#c1cdc1").Value);
            ColorCodes.Add("honeydew4", HexToColor("#838b83").Value);
            ColorCodes.Add("hotpink", HexToColor("#ff69b4").Value);
            ColorCodes.Add("hotpink1", HexToColor("#ff6eb4").Value);
            ColorCodes.Add("hotpink2", HexToColor("#ee6aa7").Value);
            ColorCodes.Add("hotpink3", HexToColor("#cd6090").Value);
            ColorCodes.Add("hotpink4", HexToColor("#8b3a62").Value);
            ColorCodes.Add("indianred", HexToColor("#cd5c5c").Value);
            ColorCodes.Add("indianred1", HexToColor("#ff6a6a").Value);
            ColorCodes.Add("indianred2", HexToColor("#ee6363").Value);
            ColorCodes.Add("indianred3", HexToColor("#cd5555").Value);
            ColorCodes.Add("indianred4", HexToColor("#8b3a3a").Value);
            ColorCodes.Add("indigo", HexToColor("#4B0082").Value);
            ColorCodes.Add("ivory", HexToColor("#fffff0").Value);
            ColorCodes.Add("ivory1", HexToColor("#fffff0").Value);
            ColorCodes.Add("ivory2", HexToColor("#eeeee0").Value);
            ColorCodes.Add("ivory3", HexToColor("#cdcdc1").Value);
            ColorCodes.Add("ivory4", HexToColor("#8b8b83").Value);
            ColorCodes.Add("khaki", HexToColor("#f0e68c").Value);
            ColorCodes.Add("khaki1", HexToColor("#fff68f").Value);
            ColorCodes.Add("khaki2", HexToColor("#eee685").Value);
            ColorCodes.Add("khaki3", HexToColor("#cdc673").Value);
            ColorCodes.Add("khaki4", HexToColor("#8b864e").Value);
            ColorCodes.Add("lavender", HexToColor("#e6e6fa").Value);
            ColorCodes.Add("lavenderblush", HexToColor("#fff0f5").Value);
            ColorCodes.Add("lavenderblush1", HexToColor("#fff0f5").Value);
            ColorCodes.Add("lavenderblush2", HexToColor("#eee0e5").Value);
            ColorCodes.Add("lavenderblush3", HexToColor("#cdc1c5").Value);
            ColorCodes.Add("lavenderblush4", HexToColor("#8b8386").Value);
            ColorCodes.Add("lawngreen", HexToColor("#7cfc00").Value);
            ColorCodes.Add("lemonchiffon", HexToColor("#fffacd").Value);
            ColorCodes.Add("lemonchiffon1", HexToColor("#fffacd").Value);
            ColorCodes.Add("lemonchiffon2", HexToColor("#eee9bf").Value);
            ColorCodes.Add("lemonchiffon3", HexToColor("#cdc9a5").Value);
            ColorCodes.Add("lemonchiffon4", HexToColor("#8b8970").Value);
            ColorCodes.Add("light", HexToColor("#eedd82").Value);
            ColorCodes.Add("lightblue", HexToColor("#add8e6").Value);
            ColorCodes.Add("lightblue1", HexToColor("#bfefff").Value);
            ColorCodes.Add("lightblue2", HexToColor("#b2dfee").Value);
            ColorCodes.Add("lightblue3", HexToColor("#9ac0cd").Value);
            ColorCodes.Add("lightblue4", HexToColor("#68838b").Value);
            ColorCodes.Add("lightcoral", HexToColor("#f08080").Value);
            ColorCodes.Add("lightcyan", HexToColor("#e0ffff").Value);
            ColorCodes.Add("lightcyan1", HexToColor("#e0ffff").Value);
            ColorCodes.Add("lightcyan2", HexToColor("#d1eeee").Value);
            ColorCodes.Add("lightcyan3", HexToColor("#b4cdcd").Value);
            ColorCodes.Add("lightcyan4", HexToColor("#7a8b8b").Value);
            ColorCodes.Add("lightgoldenrod", HexToColor("#ffec8b").Value);
            ColorCodes.Add("lightgoldenrod1", HexToColor("#ffec8b").Value);
            ColorCodes.Add("lightgoldenrod2", HexToColor("#eedc82").Value);
            ColorCodes.Add("lightgoldenrod3", HexToColor("#cdbe70").Value);
            ColorCodes.Add("lightgoldenrod4", HexToColor("#8b814c").Value);
            ColorCodes.Add("lightgoldenrodyellow", HexToColor("#fafad2").Value);
            ColorCodes.Add("lightgray", HexToColor("#d3d3d3").Value);
            ColorCodes.Add("lightpink", HexToColor("#ffb6c1").Value);
            ColorCodes.Add("lightpink1", HexToColor("#ffaeb9").Value);
            ColorCodes.Add("lightpink2", HexToColor("#eea2ad").Value);
            ColorCodes.Add("lightpink3", HexToColor("#cd8c95").Value);
            ColorCodes.Add("lightpink4", HexToColor("#8b5f65").Value);
            ColorCodes.Add("lightsalmon", HexToColor("#ffa07a").Value);
            ColorCodes.Add("lightsalmon1", HexToColor("#ffa07a").Value);
            ColorCodes.Add("lightsalmon2", HexToColor("#ee9572").Value);
            ColorCodes.Add("lightsalmon3", HexToColor("#cd8162").Value);
            ColorCodes.Add("lightsalmon4", HexToColor("#8b5742").Value);
            ColorCodes.Add("lightseagreen", HexToColor("#20b2aa").Value);
            ColorCodes.Add("lightskyblue", HexToColor("#87cefa").Value);
            ColorCodes.Add("lightskyblue1", HexToColor("#b0e2ff").Value);
            ColorCodes.Add("lightskyblue2", HexToColor("#a4d3ee").Value);
            ColorCodes.Add("lightskyblue3", HexToColor("#8db6cd").Value);
            ColorCodes.Add("lightskyblue4", HexToColor("#607b8b").Value);
            ColorCodes.Add("lightslateblue", HexToColor("#8470ff").Value);
            ColorCodes.Add("lightslategray", HexToColor("#778899").Value);
            ColorCodes.Add("lightsteelblue", HexToColor("#b0c4de").Value);
            ColorCodes.Add("lightsteelblue1", HexToColor("#cae1ff").Value);
            ColorCodes.Add("lightsteelblue2", HexToColor("#bcd2ee").Value);
            ColorCodes.Add("lightsteelblue3", HexToColor("#a2b5cd").Value);
            ColorCodes.Add("lightsteelblue4", HexToColor("#6e7b8b").Value);
            ColorCodes.Add("lightyellow", HexToColor("#ffffe0").Value);
            ColorCodes.Add("lightyellow1", HexToColor("#ffffe0").Value);
            ColorCodes.Add("lightyellow2", HexToColor("#eeeed1").Value);
            ColorCodes.Add("lightyellow3", HexToColor("#cdcdb4").Value);
            ColorCodes.Add("lightyellow4", HexToColor("#8b8b7a").Value);
            ColorCodes.Add("limegreen", HexToColor("#32cd32").Value);
            ColorCodes.Add("linen", HexToColor("#faf0e6").Value);
            ColorCodes.Add("magenta", HexToColor("#ff00ff").Value);
            ColorCodes.Add("magenta2", HexToColor("#ee00ee").Value);
            ColorCodes.Add("magenta3", HexToColor("#cd00cd").Value);
            ColorCodes.Add("magenta4", HexToColor("#8b008b").Value);
            ColorCodes.Add("maroon", HexToColor("#b03060").Value);
            ColorCodes.Add("maroon1", HexToColor("#ff34b3").Value);
            ColorCodes.Add("maroon2", HexToColor("#ee30a7").Value);
            ColorCodes.Add("maroon3", HexToColor("#cd2990").Value);
            ColorCodes.Add("maroon4", HexToColor("#8b1c62").Value);
            ColorCodes.Add("medium", HexToColor("#66cdaa").Value);
            ColorCodes.Add("mediumaquamarine", HexToColor("#66cdaa").Value);
            ColorCodes.Add("mediumblue", HexToColor("#0000cd").Value);
            ColorCodes.Add("mediumorchid", HexToColor("#ba55d3").Value);
            ColorCodes.Add("mediumorchid1", HexToColor("#e066ff").Value);
            ColorCodes.Add("mediumorchid2", HexToColor("#d15fee").Value);
            ColorCodes.Add("mediumorchid3", HexToColor("#b452cd").Value);
            ColorCodes.Add("mediumorchid4", HexToColor("#7a378b").Value);
            ColorCodes.Add("mediumpurple", HexToColor("#9370db").Value);
            ColorCodes.Add("mediumpurple1", HexToColor("#ab82ff").Value);
            ColorCodes.Add("mediumpurple2", HexToColor("#9f79ee").Value);
            ColorCodes.Add("mediumpurple3", HexToColor("#8968cd").Value);
            ColorCodes.Add("mediumpurple4", HexToColor("#5d478b").Value);
            ColorCodes.Add("mediumseagreen", HexToColor("#3cb371").Value);
            ColorCodes.Add("mediumslateblue", HexToColor("#7b68ee").Value);
            ColorCodes.Add("mediumspringgreen", HexToColor("#00fa9a").Value);
            ColorCodes.Add("mediumturquoise", HexToColor("#48d1cc").Value);
            ColorCodes.Add("mediumvioletred", HexToColor("#c71585").Value);
            ColorCodes.Add("midnightblue", HexToColor("#191970").Value);
            ColorCodes.Add("mintcream", HexToColor("#f5fffa").Value);
            ColorCodes.Add("mistyrose", HexToColor("#ffe4e1").Value);
            ColorCodes.Add("mistyrose1", HexToColor("#ffe4e1").Value);
            ColorCodes.Add("mistyrose2", HexToColor("#eed5d2").Value);
            ColorCodes.Add("mistyrose3", HexToColor("#cdb7b5").Value);
            ColorCodes.Add("mistyrose4", HexToColor("#8b7d7b").Value);
            ColorCodes.Add("moccasin", HexToColor("#ffe4b5").Value);
            ColorCodes.Add("navajowhite", HexToColor("#ffdead").Value);
            ColorCodes.Add("navajowhite1", HexToColor("#ffdead").Value);
            ColorCodes.Add("navajowhite2", HexToColor("#eecfa1").Value);
            ColorCodes.Add("navajowhite3", HexToColor("#cdb38b").Value);
            ColorCodes.Add("navajowhite4", HexToColor("#8b795e").Value);
            ColorCodes.Add("navyblue", HexToColor("#000080").Value);
            ColorCodes.Add("oldlace", HexToColor("#fdf5e6").Value);
            ColorCodes.Add("olivedrab", HexToColor("#6b8e23").Value);
            ColorCodes.Add("olivedrab1", HexToColor("#c0ff3e").Value);
            ColorCodes.Add("olivedrab2", HexToColor("#b3ee3a").Value);
            ColorCodes.Add("olivedrab4", HexToColor("#698b22").Value);
            ColorCodes.Add("orange", HexToColor("#ffa500").Value);
            ColorCodes.Add("orange1", HexToColor("#ffa500").Value);
            ColorCodes.Add("orange2", HexToColor("#ee9a00").Value);
            ColorCodes.Add("orange3", HexToColor("#cd8500").Value);
            ColorCodes.Add("orange4", HexToColor("#8b5a00").Value);
            ColorCodes.Add("orangered", HexToColor("#ff4500").Value);
            ColorCodes.Add("orangered1", HexToColor("#ff4500").Value);
            ColorCodes.Add("orangered2", HexToColor("#ee4000").Value);
            ColorCodes.Add("orangered3", HexToColor("#cd3700").Value);
            ColorCodes.Add("orangered4", HexToColor("#8b2500").Value);
            ColorCodes.Add("orchid", HexToColor("#da70d6").Value);
            ColorCodes.Add("orchid1", HexToColor("#ff83fa").Value);
            ColorCodes.Add("orchid2", HexToColor("#ee7ae9").Value);
            ColorCodes.Add("orchid3", HexToColor("#cd69c9").Value);
            ColorCodes.Add("orchid4", HexToColor("#8b4789").Value);
            ColorCodes.Add("pale", HexToColor("#db7093").Value);
            ColorCodes.Add("palegoldenrod", HexToColor("#eee8aa").Value);
            ColorCodes.Add("palegreen", HexToColor("#98fb98").Value);
            ColorCodes.Add("palegreen1", HexToColor("#9aff9a").Value);
            ColorCodes.Add("palegreen2", HexToColor("#90ee90").Value);
            ColorCodes.Add("palegreen3", HexToColor("#7ccd7c").Value);
            ColorCodes.Add("palegreen4", HexToColor("#548b54").Value);
            ColorCodes.Add("paleturquoise", HexToColor("#afeeee").Value);
            ColorCodes.Add("paleturquoise1", HexToColor("#bbffff").Value);
            ColorCodes.Add("paleturquoise2", HexToColor("#aeeeee").Value);
            ColorCodes.Add("paleturquoise3", HexToColor("#96cdcd").Value);
            ColorCodes.Add("paleturquoise4", HexToColor("#668b8b").Value);
            ColorCodes.Add("palevioletred", HexToColor("#db7093").Value);
            ColorCodes.Add("palevioletred1", HexToColor("#ff82ab").Value);
            ColorCodes.Add("palevioletred2", HexToColor("#ee799f").Value);
            ColorCodes.Add("palevioletred3", HexToColor("#cd6889").Value);
            ColorCodes.Add("palevioletred4", HexToColor("#8b475d").Value);
            ColorCodes.Add("papayawhip", HexToColor("#ffefd5").Value);
            ColorCodes.Add("peachpuff", HexToColor("#ffdab9").Value);
            ColorCodes.Add("peachpuff1", HexToColor("#ffdab9").Value);
            ColorCodes.Add("peachpuff2", HexToColor("#eecbad").Value);
            ColorCodes.Add("peachpuff3", HexToColor("#cdaf95").Value);
            ColorCodes.Add("peachpuff4", HexToColor("#8b7765").Value);
            ColorCodes.Add("pink", HexToColor("#ffc0cb").Value);
            ColorCodes.Add("pink1", HexToColor("#ffb5c5").Value);
            ColorCodes.Add("pink2", HexToColor("#eea9b8").Value);
            ColorCodes.Add("pink3", HexToColor("#cd919e").Value);
            ColorCodes.Add("pink4", HexToColor("#8b636c").Value);
            ColorCodes.Add("plum", HexToColor("#dda0dd").Value);
            ColorCodes.Add("plum1", HexToColor("#ffbbff").Value);
            ColorCodes.Add("plum2", HexToColor("#eeaeee").Value);
            ColorCodes.Add("plum3", HexToColor("#cd96cd").Value);
            ColorCodes.Add("plum4", HexToColor("#8b668b").Value);
            ColorCodes.Add("powderblue", HexToColor("#b0e0e6").Value);
            ColorCodes.Add("purple", HexToColor("#a020f0").Value);
            ColorCodes.Add("rebeccapurple", HexToColor("#663399").Value);
            ColorCodes.Add("purple1", HexToColor("#9b30ff").Value);
            ColorCodes.Add("purple2", HexToColor("#912cee").Value);
            ColorCodes.Add("purple3", HexToColor("#7d26cd").Value);
            ColorCodes.Add("purple4", HexToColor("#551a8b").Value);
            ColorCodes.Add("red1", HexToColor("#ff0000").Value);
            ColorCodes.Add("red2", HexToColor("#ee0000").Value);
            ColorCodes.Add("red3", HexToColor("#cd0000").Value);
            ColorCodes.Add("red4", HexToColor("#8b0000").Value);
            ColorCodes.Add("rosybrown", HexToColor("#bc8f8f").Value);
            ColorCodes.Add("rosybrown1", HexToColor("#ffc1c1").Value);
            ColorCodes.Add("rosybrown2", HexToColor("#eeb4b4").Value);
            ColorCodes.Add("rosybrown3", HexToColor("#cd9b9b").Value);
            ColorCodes.Add("rosybrown4", HexToColor("#8b6969").Value);
            ColorCodes.Add("royalblue", HexToColor("#4169e1").Value);
            ColorCodes.Add("royalblue1", HexToColor("#4876ff").Value);
            ColorCodes.Add("royalblue2", HexToColor("#436eee").Value);
            ColorCodes.Add("royalblue3", HexToColor("#3a5fcd").Value);
            ColorCodes.Add("royalblue4", HexToColor("#27408b").Value);
            ColorCodes.Add("saddlebrown", HexToColor("#8b4513").Value);
            ColorCodes.Add("salmon", HexToColor("#fa8072").Value);
            ColorCodes.Add("salmon1", HexToColor("#ff8c69").Value);
            ColorCodes.Add("salmon2", HexToColor("#ee8262").Value);
            ColorCodes.Add("salmon3", HexToColor("#cd7054").Value);
            ColorCodes.Add("salmon4", HexToColor("#8b4c39").Value);
            ColorCodes.Add("sandybrown", HexToColor("#f4a460").Value);
            ColorCodes.Add("seagreen", HexToColor("#54ff9f").Value);
            ColorCodes.Add("seagreen1", HexToColor("#54ff9f").Value);
            ColorCodes.Add("seagreen2", HexToColor("#4eee94").Value);
            ColorCodes.Add("seagreen3", HexToColor("#43cd80").Value);
            ColorCodes.Add("seagreen4", HexToColor("#2e8b57").Value);
            ColorCodes.Add("seashell", HexToColor("#fff5ee").Value);
            ColorCodes.Add("seashell1", HexToColor("#fff5ee").Value);
            ColorCodes.Add("seashell2", HexToColor("#eee5de").Value);
            ColorCodes.Add("seashell3", HexToColor("#cdc5bf").Value);
            ColorCodes.Add("seashell4", HexToColor("#8b8682").Value);
            ColorCodes.Add("silver", HexToColor("#c0c0c0").Value);
            ColorCodes.Add("sienna", HexToColor("#a0522d").Value);
            ColorCodes.Add("sienna1", HexToColor("#ff8247").Value);
            ColorCodes.Add("sienna2", HexToColor("#ee7942").Value);
            ColorCodes.Add("sienna3", HexToColor("#cd6839").Value);
            ColorCodes.Add("sienna4", HexToColor("#8b4726").Value);
            ColorCodes.Add("skyblue", HexToColor("#87ceeb").Value);
            ColorCodes.Add("skyblue1", HexToColor("#87ceff").Value);
            ColorCodes.Add("skyblue2", HexToColor("#7ec0ee").Value);
            ColorCodes.Add("skyblue3", HexToColor("#6ca6cd").Value);
            ColorCodes.Add("skyblue4", HexToColor("#4a708b").Value);
            ColorCodes.Add("slateblue", HexToColor("#6a5acd").Value);
            ColorCodes.Add("slateblue1", HexToColor("#836fff").Value);
            ColorCodes.Add("slateblue2", HexToColor("#7a67ee").Value);
            ColorCodes.Add("slateblue3", HexToColor("#6959cd").Value);
            ColorCodes.Add("slateblue4", HexToColor("#473c8b").Value);
            ColorCodes.Add("slategray", HexToColor("#708090").Value);
            ColorCodes.Add("slategray1", HexToColor("#c6e2ff").Value);
            ColorCodes.Add("slategray2", HexToColor("#b9d3ee").Value);
            ColorCodes.Add("slategray3", HexToColor("#9fb6cd").Value);
            ColorCodes.Add("slategray4", HexToColor("#6c7b8b").Value);
            ColorCodes.Add("snow", HexToColor("#fffafa").Value);
            ColorCodes.Add("snow1", HexToColor("#fffafa").Value);
            ColorCodes.Add("snow2", HexToColor("#eee9e9").Value);
            ColorCodes.Add("snow3", HexToColor("#cdc9c9").Value);
            ColorCodes.Add("snow4", HexToColor("#8b8989").Value);
            ColorCodes.Add("springgreen", HexToColor("#00ff7f").Value);
            ColorCodes.Add("springgreen1", HexToColor("#00ff7f").Value);
            ColorCodes.Add("springgreen2", HexToColor("#00ee76").Value);
            ColorCodes.Add("springgreen3", HexToColor("#00cd66").Value);
            ColorCodes.Add("springgreen4", HexToColor("#008b45").Value);
            ColorCodes.Add("steelblue", HexToColor("#4682b4").Value);
            ColorCodes.Add("steelblue1", HexToColor("#63b8ff").Value);
            ColorCodes.Add("steelblue2", HexToColor("#5cacee").Value);
            ColorCodes.Add("steelblue3", HexToColor("#4f94cd").Value);
            ColorCodes.Add("steelblue4", HexToColor("#36648b").Value);
            ColorCodes.Add("tan", HexToColor("#d2b48c").Value);
            ColorCodes.Add("tan1", HexToColor("#ffa54f").Value);
            ColorCodes.Add("tan2", HexToColor("#ee9a49").Value);
            ColorCodes.Add("tan3", HexToColor("#cd853f").Value);
            ColorCodes.Add("tan4", HexToColor("#8b5a2b").Value);
            ColorCodes.Add("thistle", HexToColor("#d8bfd8").Value);
            ColorCodes.Add("thistle1", HexToColor("#ffe1ff").Value);
            ColorCodes.Add("thistle2", HexToColor("#eed2ee").Value);
            ColorCodes.Add("thistle3", HexToColor("#cdb5cd").Value);
            ColorCodes.Add("thistle4", HexToColor("#8b7b8b").Value);
            ColorCodes.Add("tomato", HexToColor("#ff6347").Value);
            ColorCodes.Add("tomato1", HexToColor("#ff6347").Value);
            ColorCodes.Add("tomato2", HexToColor("#ee5c42").Value);
            ColorCodes.Add("tomato3", HexToColor("#cd4f39").Value);
            ColorCodes.Add("tomato4", HexToColor("#8b3626").Value);
            ColorCodes.Add("tropicalindigo", HexToColor("#9683EC").Value);
            ColorCodes.Add("turquoise", HexToColor("#40e0d0").Value);
            ColorCodes.Add("turquoise1", HexToColor("#00f5ff").Value);
            ColorCodes.Add("turquoise2", HexToColor("#00e5ee").Value);
            ColorCodes.Add("turquoise3", HexToColor("#00c5cd").Value);
            ColorCodes.Add("turquoise4", HexToColor("#00868b").Value);
            ColorCodes.Add("violet", HexToColor("#ee82ee").Value);
            ColorCodes.Add("violetred", HexToColor("#d02090").Value);
            ColorCodes.Add("violetred1", HexToColor("#ff3e96").Value);
            ColorCodes.Add("violetred2", HexToColor("#ee3a8c").Value);
            ColorCodes.Add("violetred3", HexToColor("#cd3278").Value);
            ColorCodes.Add("violetred4", HexToColor("#8b2252").Value);
            ColorCodes.Add("wheat", HexToColor("#f5deb3").Value);
            ColorCodes.Add("wheat1", HexToColor("#ffe7ba").Value);
            ColorCodes.Add("wheat2", HexToColor("#eed8ae").Value);
            ColorCodes.Add("wheat3", HexToColor("#cdba96").Value);
            ColorCodes.Add("wheat4", HexToColor("#8b7e66").Value);
            ColorCodes.Add("white", HexToColor("#ffffff").Value);
            ColorCodes.Add("whitesmoke", HexToColor("#f5f5f5").Value);
            ColorCodes.Add("yellow", HexToColor("#ffff00").Value);
            ColorCodes.Add("yellow1", HexToColor("#ffff00").Value);
            ColorCodes.Add("yellow2", HexToColor("#eeee00").Value);
            ColorCodes.Add("yellow3", HexToColor("#cdcd00").Value);
            ColorCodes.Add("yellow4", HexToColor("#8b8b00").Value);
            ColorCodes.Add("yellowgreen", HexToColor("#9acd32").Value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Vector2 type.
        /// </summary>
        public override ConversionResult Convert(object value, ValueConverterContext context)
        {
            if (value == null)
            {
                return base.Convert(value, context);
            }

            Type valueType = value.GetType();
            if (valueType == _type)
            {
                return base.Convert(value, context);
            }
            else if (valueType == _stringType)
            {
                var stringValue = (string)value;
                try
                {
                    // supported formats: #aarrggbb | #rrggbb | ColorName | r,g,b,a
                    Color? color = HexToColor(stringValue);
                    if (color.HasValue)
                    {
                        // hex color
                        return new ConversionResult(color.Value);
                    }
                    else
                    {
                        // is the value a color name?
                        string colorName = stringValue.Trim().ToLower();
                        if (ColorCodes.ContainsKey(colorName))
                        {
                            return new ConversionResult(ColorCodes[colorName]);
                        }
                        else
                        {
                            // no. is it a r,g,b,a value?
                            Color? rgbaColor = RGBAToColor(stringValue);
                            if (rgbaColor.HasValue)
                            {
                                return new ConversionResult(rgbaColor.Value);
                            }
                        }

                        return StringConversionFailed(value);
                    }
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            Color color = (Color)value;
            return String.Format("{0},{1},{2},{3}", color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Gets color from hex value.
        /// </summary>
        public static Color? HexToColor(string hex)
        {
            var trimmedValue = hex.Trim();
            if (trimmedValue.StartsWith("#"))
            {
                if (trimmedValue.Length == 9)
                {
                    // #aarrggbb
                    return new Color(
                        int.Parse(trimmedValue.Substring(3, 2), System.Globalization.NumberStyles.HexNumber) / 255f,
                        int.Parse(trimmedValue.Substring(5, 2), System.Globalization.NumberStyles.HexNumber) / 255f,
                        int.Parse(trimmedValue.Substring(7, 2), System.Globalization.NumberStyles.HexNumber) / 255f,
                        int.Parse(trimmedValue.Substring(1, 2), System.Globalization.NumberStyles.HexNumber) / 255f
                        );
                }
                else if (trimmedValue.Length == 7)
                {
                    // #rrggbb
                    return new Color(
                        int.Parse(trimmedValue.Substring(1, 2), System.Globalization.NumberStyles.HexNumber) / 255f,
                        int.Parse(trimmedValue.Substring(3, 2), System.Globalization.NumberStyles.HexNumber) / 255f,
                        int.Parse(trimmedValue.Substring(5, 2), System.Globalization.NumberStyles.HexNumber) / 255f
                        );
                }
            }

            return null;
        }

        /// <summary>
        /// Gets color from float RGBA values (0-1)
        /// </summary>
        public static Color? RGBAToColor(string color)
        {
            string[] values = color.Split(new char[] { ',' });
            if (values.Length == 3)
            {
                return new Color(System.Convert.ToSingle(values[0], CultureInfo.InvariantCulture),
                    System.Convert.ToSingle(values[1], CultureInfo.InvariantCulture),
                    System.Convert.ToSingle(values[2], CultureInfo.InvariantCulture));
            }
            else if (values.Length == 4)
            {
                return new Color(System.Convert.ToSingle(values[0], CultureInfo.InvariantCulture),
                    System.Convert.ToSingle(values[1], CultureInfo.InvariantCulture),
                    System.Convert.ToSingle(values[2], CultureInfo.InvariantCulture),
                    System.Convert.ToSingle(values[3], CultureInfo.InvariantCulture));
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
