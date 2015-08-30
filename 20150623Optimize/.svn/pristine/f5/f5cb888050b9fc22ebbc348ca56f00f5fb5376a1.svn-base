using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
 /// 汉字转拼音
 /// </summary>
public static class HZ2PY
{
    #region 汉字转首字母拼音缩写
    /// <summary>
    /// 汉字转首字母拼音缩写
    /// </summary>
    /// <param name="Input">要转换的汉字字符串</param>
    /// <returns>首字母拼音缩写</returns>
    public static string GetFirstPY(string Input)
    {
        string ret = "";
        foreach (char c in Input)
        {
            if ((int)c >= 32 && (int)c <= 126)
            {//字母和符号原样保留
                ret += c.ToString();
            }
            else
            {//累加拼音声母
                ret += GetPYChar(c.ToString());
            }
        }
        return ret;
    }

    /// <summary>
    /// 取单个字符的拼音声母
    /// </summary>
    /// <param name="c">要转换的单个汉字</param>
    /// <returns>拼音声母</returns>
    private static string GetPYChar(string c)
    {
        byte[] array = new byte[2];
        array = System.Text.Encoding.Default.GetBytes(c);
        int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
        if (i < 0xB0A1) return "*";
        if (i < 0xB0C5) return "A";
        if (i < 0xB2C1) return "B";
        if (i < 0xB4EE) return "C";
        if (i < 0xB6EA) return "D";
        if (i < 0xB7A2) return "E";
        if (i < 0xB8C1) return "F";
        if (i < 0xB9FE) return "G";
        if (i < 0xBBF7) return "H";
        if (i < 0xBFA6) return "J";
        if (i < 0xC0AC) return "K";
        if (i < 0xC2E8) return "L";
        if (i < 0xC4C3) return "M";
        if (i < 0xC5B6) return "N";
        if (i < 0xC5BE) return "O";
        if (i < 0xC6DA) return "P";
        if (i < 0xC8BB) return "Q";
        if (i < 0xC8F6) return "R";
        if (i < 0xCBFA) return "S";
        if (i < 0xCDDA) return "T";
        if (i < 0xCEF4) return "W";
        if (i < 0xD1B9) return "X";
        if (i < 0xD4D1) return "Y";
        if (i < 0xD7FA) return "Z";

        // 修正部分已知异常文字
        if (i == 56282) return "Z";// 修正“圳”字
        if (i == 63182) return "X";// 修正“鑫”字
        if (i == 58310) return "Y";// 修正“闫”字

        return "*";
    }
    #endregion

    #region 把汉字转化成全拼音

    #region int[],string[]
    private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183,-18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254//,-10254
        };

    private static string[] pyName = new string[]
        {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"//,"*"
        };
    #endregion

    /// <summary>
    /// 把汉字转换成拼音(全拼)
    /// </summary>
    /// <param name="hzString">汉字字符串</param>
    /// <returns>转换后的拼音(全拼)字符串</returns>
    public static string ConvertPY(string hzString)
    {
        // 匹配中文字符
        Regex regex = new Regex("^[\u4e00-\u9fa5]$");
        byte[] array = new byte[2];
        string pyString = "";
        int chrAsc = 0;
        int i1 = 0;
        int i2 = 0;
        char[] noWChar = hzString.ToCharArray();

        for (int j = 0; j < noWChar.Length; j++)
        {
            // 中文字符
            if (regex.IsMatch(noWChar[j].ToString()))
            {
                array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                i1 = (short)(array[0]);
                i2 = (short)(array[1]);
                chrAsc = i1 * 256 + i2 - 65536;
                if (chrAsc > 0 && chrAsc < 160)
                {
                    pyString += noWChar[j];
                }
                else
                {

                    switch (chrAsc)
                    {
                        //修正部分已知异常文字
                        case -9254:// 修正“圳”字
                            pyString += "Zhen"; break;
                        case -2354:// 修正“鑫”字
                            pyString += "Xin"; break;
                        case -7226:// 修正“闫”字
                            pyString += "Yan"; break;
                        default:
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    //if (pyName[i] == "*")
                                    //    pyString += noWChar[j].ToString();
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                            break;
                    }

                }
            }
            // 非中文字符
            else
            {
                pyString += noWChar[j].ToString();
            }
        }
        return pyString;
    }
    #endregion

    #region 未修正文字,常用待整理
    //丬亘亵伛伥伧伫佥侪侬俣俦俨俪偬偻偾傥傧傩兖冁冢凫凼刍刭刿剀劢勐匦匮卺厍厣厮叽吣呒呓呖呗呙咔咛咝咤咴哌哒哓哔哕哙哜哝唛唠
    //唢唣唿啧啬啭喽喾嗫嗳嘤噜噼嚯囵圹坂坜垅垆垧垩垭垲垴埘埙埚埝埯奁奂妩妪妫姗娅娆娈娲娴婵媪嫒嫔嫱嬷尴屦岖岘岙岚岽峄峤峥崂崃
    //嵘嵛嵝嵴巅巯帏帱帻帼幞庑廪弪徕忏忾怃怄怅怆怼怿恸恹恺恻恽悫悭惬愍愠愦憷懑懔戆戋戗戬扪抟挢掴掼揸揿摅撄撷撸撺斓昙晔晖暧杩
    //枞枥枧枨枭柽栀栉栊栌栎栾桊桠桡桢桤桦桧棂椁椟椠椤榄榇榈榉槟槠樯橥橹橼檐檩欤殁殇殒殓殚殡毂毵氇氩氲沓沣沩泶泷泸泺泾浃浈浍
    //浏浒浔涞涠渌渎渑渖溆滗滟滠滢漤潆潇潋潴濑灏炀炖炜炝烨焖焘煅煳熘牍牦犟犷犸狍狯狲猃猕猡猬玑玮玺珉珏珑珲琏瑷璇璎瓒瓯畲疖疠
    //疬疱疴痖痨痫瘅瘗瘘瘾瘿癞癫癯皲眍眦睐睑矶砀砗砜砺砻硖硗碛碜碹磙祢祯禀禅稆稣穑窦窭笃笕笾筚筝箦箧箨箪箫篑簖籁籴籼粜粝糁糇
    //絷纟纡纣纥纨纩纭纰纾绀绁绂绉绋绌绐绔绗绛绠绡绨绫绮绯绱绲绶绺绻绾缁缂缃缇缈缋缌缍缏缑缒缗缙缛缜缟缡缢缣缤缥缦缧缪缫缬缭
    //缯缰缱缲缳缵罂罴羁羟耢耧聍聩肷胧胨胪胫脍脔脶腌腭腼腽膑舣舻艹芈芗苁苈苋苌苎苘茏茑茔茕荛荜荞荟荠荥荦荨荩荪荬荭荮莅莜莳莴
    //莶莸莺莼萦蒇蒉蒌蓠蓣蓥蓦蔹蔺蕲薮藁藓虬虮虿蚝蚬蛎蛏蛱蛲蛳蛴蝈蝼蝾螨衮袅裆裢裣裥褛褴襁觇觊觋觌觎觏觐觑觞觯讠讦讧讪讴讵讷
    //诂诃诋诎诏诒诓诔诖诘诙诜诟诠诤诨诩诮诰诳诶诹诼诿谀谂谄谇谌谏谑谒谔谕谖谘谙谛谝谟谠谡谥谧谪谫谮谯谲谳谵谶豮贠贲贳贶贻贽
    //赀赅赆赇赈赉赍赑赒赓赕赗赙赜赝赟赪趱趸跄跖跞跶跷跸跹跻踬踯蹑蹒蹰躏躜轪轫轭轱轲轳轵轶轷轸轹轺轼轾辀辁辂辄辇辋辌辍辎辏辒
    //辔辘辚迩迳逦邝邬邺郄郏郐郓郦酦酽酾鉅銮錾钆钇钊钋钌钍钏钐钑钔钕钖钗钘钚钛钣钤钪钫钬钭钯钰钲钴钶钷钸钹钺钼钽钿铄铈铉铊铋
    //铍铎铏铐铑铒铕铗铘铙铚铛铞铟铠铢铤铥铦铧铨铪铫铮铯铳铴铵铷铹铻铼铽铿锂锃锆锇锉锊锍锎锏锒锓锔锕锖锜锞锟锠锢锩锫锬锱锲锳
    //锴锵锶锷锸锺锼锽锾锿镂镃镆镈镉镌镎镏镒镕镖镗镙镚镛镝镞镟镠镡镢镤镥镦镧镨镩镪镫镬镮镯镱镲镳镴闩闬闱闳闵闶闼闾闿阃阄阆阇
    //阈阊阋阌阍阏阒阓阕阖阗阘阙阚阛陉陧隽雠雳霁霭靓靥鞑鞒鞯鞴韨韪韫韬顸顼颀颃颉颋颌颍颎颏颒颔颕颙颚颛颞颟颡颢颣颥颦飏飐飑飒
    //飓飔飕飖飗飙飚飨餍饤饦饧饨饩饪饫饬饳饴饷饸饹饻饽饾馀馂馃馄馇馉馊馌馍馎馐馑馓馔馕驲驵驷驸驺驽驿骀骁骃骅骈骉骊骍骎骐骒骓
    //骔骕骖骘骙骛骜骝骞骟骠骢骣骥骦骧髅髋髌鬓魇魉鱽鱾鱿鲀鲂鲄鲅鲆鲇鲈鲉鲊鲋鲌鲎鲏鲐鲑鲒鲓鲔鲕鲖鲗鲘鲙鲚鲛鲝鲞鲟鲠鲡鲢鲣鲥鲦
    //鲧鲨鲩鲪鲫鲬鲭鲮鲯鲰鲱鲲鲳鲴鲵鲶鲷鲹鲺鲻鲼鲽鲾鲿鳀鳁鳂鳄鳅鳆鳇鳈鳉鳊鳋鳌鳍鳎鳏鳐鳑鳒鳓鳔鳕鳗鳘鳙鳛鳜鳝鳟鳠鳡鳢鳣鸠鸢鸤
    //鸧鸨鸩鸪鸫鸬鸮鸰鸱鸲鸴鸶鸷鸸鸹鸺鸻鸼鸾鹀鹁鹂鹄鹆鹇鹈鹉鹋鹌鹍鹎鹐鹑鹒鹓鹔鹕鹖鹗鹘鹚鹛鹜鹝鹞鹟鹠鹡鹢鹣鹥鹦鹧鹨鹩鹪鹫鹬鹭
    //鹯鹱鹲鹳鹴鹾麸黉黡黩黪黾鼋鼌鼍鼗鼹齄齑龀龁龂龃龅龆龇龈龉龊龌龛
    #endregion
}
