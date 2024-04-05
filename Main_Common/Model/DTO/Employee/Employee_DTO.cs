using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.Employee
{
    /// <summary>>
    /// 【DTO】客戶
    /// </summary>
    public class Employee_DTO
    {
        /// <summary>
        /// 客戶代號
        /// </summary>
        [Required]
        [Display(Name = "客戶代號")]
        public string CUS_NO { get; set; }
        /// <summary>
        /// 對象別
        /// </summary>
        [Required]
        [Display(Name = "對象別")]
        public string OBJ_ID { get; set; }
        /// <summary>
        /// 區域
        /// </summary>
        [Display(Name = "區域")]
        public string CUS_ARE { get; set; }
        /// <summary>
        /// 全稱
        /// </summary>
        [Required]
        [Display(Name = "全稱")]
        public string NAME { get; set; }
        /// <summary>
        /// 簡稱
        /// </summary>
        [Required]
        [Display(Name = "簡稱")]
        public string SNM { get; set; }
        /// <summary>
        /// 負責人
        /// </summary>
        [Display(Name = "負責人")]
        public string BOS_NM { get; set; }
        /// <summary>
        /// 聯絡人1
        /// </summary>
        [Display(Name = "聯絡人1")]
        public string CNT_MAN1 { get; set; }
        /// <summary>
        /// 聯絡人2
        /// </summary>
        [Display(Name = "聯絡人2")]
        public string CNT_MAN2 { get; set; }
        /// <summary>
        /// 會計
        /// </summary>
        [Display(Name = "會計")]
        public string ACC_MAN { get; set; }
        /// <summary>
        /// 電話1
        /// </summary>
        [Display(Name = "電話1")]
        public string TEL1 { get; set; }
        /// <summary>
        /// 電話2
        /// </summary>
        [Display(Name = "電話2")]
        public string TEL2 { get; set; }
        /// <summary>
        /// 統一編碼
        /// </summary>
        [Display(Name = "統一編碼")]
        public string UNI_NO { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Display(Name = "傳真")]
        public string FAX { get; set; }
        /// <summary>
        /// 採購計劃列印周數
        /// </summary>
        [Display(Name = "採購計劃列印周數")]
        public Nullable<short> PRTWEEKS_PO { get; set; }
        /// <summary>
        /// 確定採購訂單周數
        /// </summary>
        [Display(Name = "確定採購訂單周數")]
        public Nullable<short> SOWEEKS_PO { get; set; }
        /// <summary>
        /// 行業別
        /// </summary>
        [Display(Name = "行業別")]
        public string BIZ_DSC { get; set; }
        /// <summary>
        /// 英文名稱
        /// </summary>
        [Display(Name = "英文名稱")]
        public string NM_ENG { get; set; }
        /// <summary>
        /// 歸屬對象
        /// </summary>
        [Display(Name = "歸屬對象")]
        public string MAS_CUS { get; set; }
        /// <summary>
        /// 保留!
        /// </summary>
        [Display(Name = "保留!")]
        public Nullable<short> PAY_DD { get; set; }
        /// <summary>
        /// 結帳類別
        /// </summary>
        [Required]
        [Display(Name = "結帳類別")]
        public string CLS_MTH { get; set; }
        /// <summary>
        /// 起算日
        /// </summary>
        [Required]
        [Display(Name = "起算日")]
        public Nullable<short> CLS_DD { get; set; }
        /// <summary>
        /// 間隔天數
        /// </summary>
        [Required]
        [Display(Name = "間隔天數")]
        public Nullable<short> MM_END { get; set; }
        /// <summary>
        /// 票據到期日
        /// </summary>
        [Required]
        [Display(Name = "票據到期日")]
        public Nullable<short> CHK_DD { get; set; }
        /// <summary>
        /// 聯絡人1職稱
        /// </summary>
        [Display(Name = "聯絡人1職稱")]
        public string CNT_JOB1 { get; set; }
        /// <summary>
        /// 聯絡人2職稱
        /// </summary>
        [Display(Name = "聯絡人2職稱")]
        public string CNT_JOB2 { get; set; }
        /// <summary>
        /// 責任業務
        /// </summary>
        [Display(Name = "責任業務")]
        public string SAL { get; set; }
        /// <summary>
        /// 發票地址
        /// </summary>
        [Display(Name = "發票地址")]
        public string ADR1 { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Display(Name = "公司地址")]
        public string ADR2 { get; set; }
        /// <summary>
        /// 英文地址
        /// </summary>
        [Display(Name = "英文地址")]
        public string ADR_ENG { get; set; }
        /// <summary>
        /// 信用額度
        /// </summary>
        [Display(Name = "信用額度")]
        public Nullable<decimal> LIM_NR { get; set; }
        /// <summary>
        /// 信用管制
        /// </summary>
        [Required]
        [Display(Name = "信用管制")]
        public string CRD_ID { get; set; }
        /// <summary>
        /// 發票類別
        /// </summary>
        [Display(Name = "發票類別")]
        public string INV_ID { get; set; }
        /// <summary>
        /// 扣稅類別
        /// </summary>
        [Required]
        [Display(Name = "扣稅類別")]
        public string ID1_TAX { get; set; }
        /// <summary>
        /// 按外幣沖銷否
        /// </summary>
        [Required]
        [Display(Name = "按外幣沖銷否")]
        public string ID2_TAX { get; set; }
        /// <summary>
        /// 立帳方式
        /// </summary>
        [Required]
        [Display(Name = "立帳方式")]
        public string CLS2 { get; set; }
        /// <summary>
        /// 起始往來日期
        /// </summary>
        [Display(Name = "起始往來日期")]
        public Nullable<System.DateTime> STR_DD { get; set; }
        /// <summary>
        /// 截止往來日期
        /// </summary>
        [Display(Name = "截止往來日期")]
        public Nullable<System.DateTime> END_DD { get; set; }
        /// <summary>
        /// 使用幣別
        /// </summary>
        [Display(Name = "使用幣別")]
        public string CUR { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        [Display(Name = "摘要")]
        public string REM { get; set; }
        /// <summary>
        /// 電子郵件
        /// </summary>
        [Display(Name = "電子郵件")]
        public string E_MAIL { get; set; }
        /// <summary>
        /// 送貨方式
        /// </summary>
        [Display(Name = "送貨方式")]
        public string SEND_MTH { get; set; }
        /// <summary>
        /// 送貨倉
        /// </summary>
        [Display(Name = "送貨倉")]
        public string SEND_WH { get; set; }
        /// <summary>
        /// 保留!
        /// </summary>
        [Display(Name = "保留!")]
        public string BNK_NAME { get; set; }
        /// <summary>
        /// 銀行帳號
        /// </summary>
        [Display(Name = "銀行帳號")]
        public string ID_CODE { get; set; }
        /// <summary>
        /// 客戶等級
        /// </summary>
        [Required]
        [Display(Name = "客戶等級")]
        public string Cus_Level { get; set; }
        /// <summary>
        /// 結帳方式
        /// </summary>
        [Display(Name = "結帳方式")]
        public string CLS1 { get; set; }
        /// <summary>
        /// 郵政編碼
        /// </summary>
        [Display(Name = "郵政編碼")]
        public string ZIP { get; set; }
        /// <summary>
        /// 公司網站
        /// </summary>
        [Display(Name = "公司網站")]
        public string COMPNET { get; set; }
        /// <summary>
        /// 成立時間
        /// </summary>
        [Display(Name = "成立時間")]
        public Nullable<System.DateTime> COMP_DD { get; set; }
        /// <summary>
        /// 資本總額
        /// </summary>
        [Display(Name = "資本總額")]
        public Nullable<decimal> CAPSUM { get; set; }
        /// <summary>
        /// 營業總額
        /// </summary>
        [Display(Name = "營業總額")]
        public Nullable<decimal> BUSISUM { get; set; }
        /// <summary>
        /// 員工數
        /// </summary>
        [Display(Name = "員工數")]
        public Nullable<int> SALMS { get; set; }
        /// <summary>
        /// 營業性質
        /// </summary>
        [Display(Name = "營業性質")]
        public string BUSINOTE { get; set; }
        /// <summary>
        /// 經營項目
        /// </summary>
        [Display(Name = "經營項目")]
        public string WORKITM { get; set; }
        /// <summary>
        /// 主要產品
        /// </summary>
        [Display(Name = "主要產品")]
        public string MAIN_PRD { get; set; }
        /// <summary>
        /// 信用含票據否
        /// </summary>
        [Required]
        [Display(Name = "信用含票據否")]
        public string CHK_CRD { get; set; }
        /// <summary>
        /// 到港聯絡人
        /// </summary>
        [Display(Name = "到港聯絡人")]
        public string NOTIFY { get; set; }
        /// <summary>
        /// 報關行
        /// </summary>
        [Display(Name = "報關行")]
        public string BROKER { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        [Display(Name = "船名")]
        public string VESSEL { get; set; }
        /// <summary>
        /// 裝貨港/收貨地
        /// </summary>
        [Display(Name = "裝貨港 / 收貨地")]
        public string LOADING_PORT { get; set; }
        /// <summary>
        /// 卸貨港/目的地
        /// </summary>
        [Display(Name = "卸貨港 / 目的地")]
        public string DIS_PORT { get; set; }
        /// <summary>
        /// 產地
        /// </summary>
        [Display(Name = "產地")]
        public string MANU_PLACE { get; set; }
        /// <summary>
        /// 銀行代號
        /// </summary>
        [Display(Name = "銀行代號")]
        public string BANK_NO { get; set; }
        /// <summary>
        /// 應收帳款科目
        /// </summary>
        [Display(Name = "應收帳款科目")]
        public string ACC_NO_AR { get; set; }
        /// <summary>
        /// 應付帳款科目
        /// </summary>
        [Display(Name = "應付帳款科目")]
        public string ACC_NO_AP { get; set; }
        /// <summary>
        /// 應收票據科目
        /// </summary>
        [Display(Name = "應收票據科目")]
        public string ACC_NO_R0 { get; set; }
        /// <summary>
        /// 應付票據科目
        /// </summary>
        [Display(Name = "應付票據科目")]
        public string ACC_NO_P0 { get; set; }
        /// <summary>
        /// 信用額度含訂單否
        /// </summary>
        [Required]
        [Display(Name = "信用額度含訂單否")]
        public string SO_CRD { get; set; }
        /// <summary>
        /// 發票名稱
        /// </summary>
        [Display(Name = "發票名稱")]
        public string FP_NAME { get; set; }
        /// <summary>
        /// 信用額度含傳真匯款否
        /// </summary>
        [Required]
        [Display(Name = "信用額度含傳真匯款否")]
        public string CHK_FAX { get; set; }
        /// <summary>
        /// 信用檢測
        /// </summary>
        [Required]
        [Display(Name = "信用檢測")]
        public string CHK_CUS_IDX { get; set; }
        /// <summary>
        /// 承運單位否
        /// </summary>
        [Required]
        [Display(Name = "承運單位否")]
        public string CY_ID { get; set; }
        /// <summary>
        /// 企業註冊編碼
        /// </summary>
        [Display(Name = "企業註冊編碼")]
        public string REGIST_CODE { get; set; }
        /// <summary>
        /// 企業類型
        /// </summary>
        [Display(Name = "企業類型")]
        public string CORP_ID { get; set; }
        /// <summary>
        /// 主管海關
        /// </summary>
        [Display(Name = "主管海關")]
        public string M_CUST { get; set; }
        /// <summary>
        /// 審核部門
        /// </summary>
        [Display(Name = "審核部門")]
        public string CHK_DEP { get; set; }
        /// <summary>
        /// 主報關員
        /// </summary>
        [Display(Name = "主報關員")]
        public string SAL_NO { get; set; }
        /// <summary>
        /// 扣款比例
        /// </summary>
        [Display(Name = "扣款比例")]
        public Nullable<decimal> RTO_KK { get; set; }
        /// <summary>
        /// 所屬部門
        /// </summary>
        [Display(Name = "所屬部門")]
        public string DEP { get; set; }
        /// <summary>
        /// 返利比率
        /// </summary>
        [Display(Name = "返利比率")]
        public Nullable<decimal> RTO_FL { get; set; }
        /// <summary>
        /// 預留比率
        /// </summary>
        [Display(Name = "預留比率")]
        public Nullable<decimal> RTO_YL { get; set; }
        /// <summary>
        /// 集團分公司
        /// </summary>
        [Display(Name = "集團分公司")]
        public string DEP1 { get; set; }
        /// <summary>
        /// 客戶稅率
        /// </summary>
        [Display(Name = "客戶稅率")]
        public Nullable<decimal> RTO_TAX { get; set; }
        /// <summary>
        /// 信用額度含下屬
        /// </summary>
        [Required]
        [Display(Name = "信用額度含下屬")]
        public string CHK_INCLUDE { get; set; }
        /// <summary>
        /// 信用額度含預收款否
        /// </summary>
        [Required]
        [Display(Name = "信用額度含預收款否")]
        public string CHK_IRP { get; set; }
        /// <summary>
        /// 信用額度含暫收款否
        /// </summary>
        [Required]
        [Display(Name = "信用額度含暫收款否")]
        public string CHK_TRP { get; set; }
        /// <summary>
        /// 審核人
        /// </summary>
        [Required]
        [Display(Name = "審核人")]
        public string CHK_MAN { get; set; }
        /// <summary>
        /// 審核日期
        /// </summary>
        [Required]
        [Display(Name = "審核日期")]
        public Nullable<System.DateTime> CLS_DATE { get; set; }
        /// <summary>
        /// 交易管制方式一
        /// </summary>
        [Required]
        [Display(Name = "交易管制方式一")]
        public string CHK_PAY1 { get; set; }
        /// <summary>
        /// 帳齡天數
        /// </summary>
        [Display(Name = "帳齡天數")]
        public Nullable<short> PAY_DAYS { get; set; }
        /// <summary>
        /// 帳齡計算方式
        /// </summary>
        [Required]
        [Display(Name = "帳齡計算方式")]
        public string PAY_FLAG { get; set; }
        /// <summary>
        /// 交易管制方式二
        /// </summary>
        [Required]
        [Display(Name = "交易管制方式二")]
        public string CHK_PAY2 { get; set; }
        /// <summary>
        /// 交易管制方式三
        /// </summary>
        [Required]
        [Display(Name = "交易管制方式三")]
        public string CHK_PAY3 { get; set; }
        /// <summary>
        /// 訂貨金額上限
        /// </summary>
        [Display(Name = "訂貨金額上限")]
        public Nullable<decimal> AMTN_MAX_PAY { get; set; }
        /// <summary>
        /// 欠款控制
        /// </summary>
        [Required]
        [Display(Name = "欠款控制")]
        public string CHK_QK { get; set; }
        /// <summary>
        /// 欠款額度
        /// </summary>
        [Display(Name = "欠款額度")]
        public Nullable<decimal> AMTN_QK { get; set; }
        /// <summary>
        /// 輸入員
        /// </summary>
        [Required]
        [Display(Name = "輸入員")]
        public string USR1 { get; set; }
        /// <summary>
        /// 輸入日期
        /// </summary>
        [Required]
        [Display(Name = "輸入日期")]
        public Nullable<System.DateTime> SYS_DATE { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        [Display(Name = "折扣率")]
        public Nullable<decimal> RTO_DISCNT { get; set; }
        /// <summary>
        /// 收款含預/暫收款否
        /// </summary>
        [Required]
        [Display(Name = "收款含預 / 暫收款否")]
        public string CHK_IRP2 { get; set; }
        /// <summary>
        /// 是否登陸用戶
        /// </summary>
        [Display(Name = "是否登陸用戶")]
        public string LOGON { get; set; }
        /// <summary>
        /// 所屬倉庫
        /// </summary>
        [Display(Name = "所屬倉庫")]
        public string WH_NO { get; set; }
        /// <summary>
        /// 停止訂貨
        /// </summary>
        [Display(Name = "停止訂貨")]
        public string STOP_ORDER { get; set; }
        /// <summary>
        /// 是否控制退貨
        /// </summary>
        [Display(Name = "是否控制退貨")]
        public string RTN_CTRL { get; set; }
        /// <summary>
        /// 是否分經/銷商
        /// </summary>
        [Display(Name = "是否分經 / 銷商")]
        public string DRP_ID { get; set; }
        /// <summary>
        /// 執行分銷定價政策
        /// </summary>
        [Display(Name = "執行分銷定價政策")]
        public string UPR4_ID { get; set; }
        /// <summary>
        /// 要貨庫位1
        /// </summary>
        [Display(Name = "要貨庫位1")]
        public string YH_WH1 { get; set; }
        /// <summary>
        /// 要貨庫位2
        /// </summary>
        [Display(Name = "要貨庫位2")]
        public string YH_WH2 { get; set; }
        /// <summary>
        /// 國家
        /// </summary>
        [Display(Name = "國家")]
        public string COUNTRY { get; set; }
        /// <summary>
        /// 預收帳款科目
        /// </summary>
        [Display(Name = "預收帳款科目")]
        public string ACC_NO_IR { get; set; }
        /// <summary>
        /// 預付帳款科目
        /// </summary>
        [Display(Name = "預付帳款科目")]
        public string ACC_NO_IP { get; set; }
        /// <summary>
        /// 採購確認
        /// </summary>
        [Display(Name = "採購確認")]
        public string CHK_DRP1 { get; set; }
        /// <summary>
        /// 收料確認
        /// </summary>
        [Display(Name = "收料確認")]
        public string CHK_DRP2 { get; set; }
        /// <summary>
        /// 託工確認
        /// </summary>
        [Display(Name = "託工確認")]
        public string CHK_DRP3 { get; set; }
        /// <summary>
        /// 性能分類
        /// </summary>
        [Display(Name = "性能分類")]
        public string XN_NO { get; set; }
        /// <summary>
        /// 條形碼控制
        /// </summary>
        [Required]
        [Display(Name = "條形碼控制")]
        public string CHK_BARCODE { get; set; }
        /// <summary>
        /// 暫估應付帳款科目
        /// </summary>
        [Display(Name = "暫估應付帳款科目")]
        public string ACC_NO_ZP { get; set; }
        /// <summary>
        /// 是否豁免
        /// </summary>
        [Display(Name = "是否豁免")]
        public string HM_ID { get; set; }
        /// <summary>
        /// 返利金額
        /// </summary>
        [Display(Name = "返利金額")]
        public Nullable<decimal> AMTN_FL { get; set; }
        /// <summary>
        /// 已返利金額
        /// </summary>
        [Display(Name = "已返利金額")]
        public Nullable<decimal> AMTN_FLED { get; set; }
        /// <summary>
        /// AMTN_FLYE
        /// </summary>
        [Display(Name = "AMTN_FLYE")]
        public Nullable<decimal> AMTN_FLYE { get; set; }
        /// <summary>
        /// 計算銷貨退回率否
        /// </summary>
        [Display(Name = "計算銷貨退回率否")]
        public string CHK_SBRTO { get; set; }
        /// <summary>
        /// 部門群組代號
        /// </summary>
        [Display(Name = "部門群組代號")]
        public string DEPRO_NO { get; set; }
        /// <summary>
        /// 首付比例
        /// </summary>
        [Display(Name = "首付比例")]
        public Nullable<decimal> RTO_FQSK { get; set; }
        /// <summary>
        /// 首付日期方式
        /// </summary>
        [Required]
        [Display(Name = "首付日期方式")]
        public string DATEFLAG_FQSK { get; set; }
        /// <summary>
        /// 首付指定日期
        /// </summary>
        [Display(Name = "首付指定日期")]
        public Nullable<System.DateTime> DATE_FQSK { get; set; }
        /// <summary>
        /// 期數
        /// </summary>
        [Display(Name = "期數")]
        public Nullable<int> QS_FQSK { get; set; }
        /// <summary>
        /// 帳戶名稱
        /// </summary>
        [Required]
        [Display(Name = "帳戶名稱")]
        public string CODE_NAME { get; set; }
        /// <summary>
        /// 快遞否
        /// </summary>
        [Required]
        [Display(Name = "快遞否")]
        public string CHK_KD { get; set; }
        /// <summary>
        /// WS客戶對應的註冊碼
        /// </summary>
        [Display(Name = "WS客戶對應的註冊碼")]
        public string WS_CUS_NO { get; set; }
        /// <summary>
        /// 納稅人識別號
        /// </summary>
        [Display(Name = "納稅人識別號")]
        public string NSR_CODE { get; set; }
        /// <summary>
        /// 助記碼
        /// </summary>
        [Required]
        [Display(Name = "助記碼")]
        public string NAME_PY { get; set; }
        /// <summary>
        /// 應收其他帳款
        /// </summary>
        [Display(Name = "應收其他帳款")]
        public string ACC_NO_AR2 { get; set; }
        /// <summary>
        /// 應付費用
        /// </summary>
        [Display(Name = "應付費用")]
        public string ACC_NO_AP2 { get; set; }
        /// <summary>
        /// 評估結果
        /// </summary>
        [Display(Name = "評估結果")]
        public string PGJG { get; set; }
        /// <summary>
        /// 攤提方式設定
        /// </summary>
        [Display(Name = "攤提方式設定")]
        public string TT_TYPE_SET { get; set; }
        /// <summary>
        /// 審核模板
        /// </summary>
        [Display(Name = "審核模板")]
        public string MOB_ID { get; set; }
        /// <summary>
        /// 出口客戶否
        /// </summary>
        [Display(Name = "出口客戶否")]
        public string EX_TRD_ID { get; set; }
        /// <summary>
        /// 進出口廠商類別
        /// </summary>
        [Display(Name = "進出口廠商類別")]
        public string IM_TRD_ID { get; set; }
        /// <summary>
        /// 信用管制含不立帳銷/退/折單
        /// </summary>
        [Required]
        [Display(Name = "信用管制含不立帳銷 / 退 / 折單")]
        public string CHK_ZHANG_ID2 { get; set; }
        /// <summary>
        /// 電子發票類型(1.B2B 2.B2C 3.B2G)
        /// </summary>
        [Display(Name = "電子發票類型(1.B2B 2.B2C 3.B2G)")]
        public string FP_TYPE { get; set; }
        /// <summary>
        /// 檢測最低消費額
        /// </summary>
        [Display(Name = "檢測最低消費額")]
        public string CHK_MIN_XF { get; set; }
        /// <summary>
        /// 檢測最低消費單據
        /// </summary>
        [Display(Name = "檢測最低消費單據")]
        public string BIL_MIN_XF { get; set; }
        /// <summary>
        /// 檢測最低消費類型
        /// </summary>
        [Required]
        [Display(Name = "檢測最低消費類型")]
        public string CHK_TYPE_MINXF { get; set; }
        /// <summary>
        /// 最低消費額
        /// </summary>
        [Display(Name = "最低消費額")]
        public Nullable<decimal> AMTN_MIN_XF { get; set; }
        /// <summary>
        /// 啟用對帳流程
        /// </summary>
        [Display(Name = "啟用對帳流程")]
        public string DJ_LC { get; set; }
        /// <summary>
        /// 啟用供應商付款申請流程
        /// </summary>
        [Display(Name = "啟用供應商付款申請流程")]
        public string DJ_SQ { get; set; }
        /// <summary>
        /// 凍結付款
        /// </summary>
        [Display(Name = "凍結付款")]
        public string DJ_PAY { get; set; }
        /// <summary>
        /// 凍結本位幣
        /// </summary>
        [Display(Name = "凍結本位幣")]
        public Nullable<decimal> AMTN_DJ { get; set; }
        /// <summary>
        /// 信用額度含出庫
        /// </summary>
        [Required]
        [Display(Name = "信用額度含出庫")]
        public string CHK_CK { get; set; }
        /// <summary>
        /// 分銷等級
        /// </summary>
        [Display(Name = "分銷等級")]
        public string FX_LEVEL { get; set; }
        /// <summary>
        /// 啟用會員載具否
        /// </summary>
        [Display(Name = "啟用會員載具否")]
        public string ISCUSTKEY { get; set; }
        /// <summary>
        /// 納稅資格
        /// </summary>
        [Display(Name = "納稅資格")]
        public string NSZG_FLAG { get; set; }
        /// <summary>
        /// 檢測供應商餘額
        /// </summary>
        [Display(Name = "檢測供應商餘額")]
        public string DJ_YE { get; set; }
        /// <summary>
        /// 銀行名稱
        /// </summary>
        [Display(Name = "銀行名稱")]
        public string BANK_NAME { get; set; }
        /// <summary>
        /// 臨時供應商
        /// </summary>
        [Display(Name = "臨時供應商")]
        public string TEMPFLAG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "")]
        public string CHK_TH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public string B2BTYPE { get; set; }
        /// <summary>
        /// 進貨檢驗設定
        /// </summary>
        [Display(Name = "進貨檢驗設定")]
        public string CHK_PC { get; set; }
        /// <summary>
        /// 托工檢驗設定
        /// </summary>
        [Display(Name = "托工檢驗設定")]
        public string CHK_TW { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "")]
        public Nullable<System.DateTime> MODIFY_DD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "")]
        public string MODIFY_MAN { get; set; }
        /// <summary>
        /// 啟用供應商送貨管控
        /// </summary>
        [Display(Name = "啟用供應商送貨管控")]
        public string CHK_SHGK { get; set; }
        /// <summary>
        /// 送貨周期
        /// </summary>
        [Display(Name = "送貨周期")]
        public Nullable<int> SHZQ_M { get; set; }
        /// <summary>
        /// 不合格次數計算方式
        /// </summary>
        [Display(Name = "不合格次數計算方式")]
        public string LOST_TYPE { get; set; }
        /// <summary>
        /// 不合格次數
        /// </summary>
        [Display(Name = "不合格次數")]
        public Nullable<int> LOST_ITMES { get; set; }
        /// <summary>
        /// 停止送貨
        /// </summary>
        [Display(Name = "停止送貨")]
        public string CHK_TZSH { get; set; }
        /// <summary>
        /// 不合格次數統計依據
        /// </summary>
        [Display(Name = "不合格次數統計依據")]
        public string LOST_CTYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public string FLAG_TAXCHG_I { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public string FLAG_TAXCHG_O { get; set; }
        /// <summary>
        /// 客戶等級價率
        /// </summary>
        [Display(Name = "客戶等級價率")]
        public Nullable<decimal> RTO_DISCNT_LEVEL { get; set; }
        /// <summary>
        /// 檢測最低採購額
        /// </summary>
        [Display(Name = "檢測最低採購額")]
        public string CHK_MIN_CG { get; set; }
        /// <summary>
        /// 檢測最低採購單據
        /// </summary>
        [Display(Name = "檢測最低採購單據")]
        public string BIL_MIN_CG { get; set; }
        /// <summary>
        /// 檢測最低採購類型
        /// </summary>
        [Required]
        [Display(Name = "檢測最低採購類型")]
        public string CHK_TYPE_MINCG { get; set; }
        /// <summary>
        /// 最低採購額
        /// </summary>
        [Display(Name = "最低採購額")]
        public Nullable<decimal> AMTN_MIN_CG { get; set; }
        /// <summary>
        /// 廠商等級
        /// </summary>
        [Display(Name = "廠商等級")]
        public string SUP_LEVEL { get; set; }
        /// <summary>
        /// WMS採購收貨流程
        /// </summary>
        [Display(Name = "WMS採購收貨流程")]
        public string WMS_SHLC_PO { get; set; }
        /// <summary>
        /// WMS委外收貨流程
        /// </summary>
        [Display(Name = "WMS委外收貨流程")]
        public string WMS_SHLC_TW { get; set; }


    }
}
