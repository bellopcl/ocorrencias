using System.Data.Entity;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping;
using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public partial class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context()
            : base("Name=Context")
        {
        }

        public DbSet<N0000AGE> N0000AGE { get; set; }
        public DbSet<N0000ERR> N0000ERR { get; set; }
        public DbSet<N0000LOG> N0000LOG { get; set; }
        public DbSet<N0000REL> N0000REL { get; set; }
        public DbSet<N0001EMPModel> N0001EMP { get; set; }
        public DbSet<N0001EMP_HIS> N0001EMP_HIS { get; set; }
        public DbSet<N0002FIL> N0002FIL { get; set; }
        public DbSet<N0003ARM> N0003ARM { get; set; }
        public DbSet<N0005TNS> N0005TNS { get; set; }
        public DbSet<N0006DER> N0006DER { get; set; }
        public DbSet<N0006FAM> N0006FAM { get; set; }
        public DbSet<N0006ORI> N0006ORI { get; set; }
        public DbSet<N0006PROModel> N0006PRO { get; set; }
        public DbSet<N0007UNI> N0007UNI { get; set; }
        public DbSet<N0008CNV> N0008CNV { get; set; }
        public DbSet<N0009ITP> N0009ITP { get; set; }
        public DbSet<N0009TAB> N0009TAB { get; set; }
        public DbSet<N0009VLD> N0009VLD { get; set; }
        public DbSet<N0010UCT> N0010UCT { get; set; }
        public DbSet<N0011CLI> N0011CLI { get; set; }
        public DbSet<N0011ENT> N0011ENT { get; set; }
        public DbSet<N0012LVM> N0012LVM { get; set; }
        public DbSet<N0012MOT> N0012MOT { get; set; }
        public DbSet<N0012TRA> N0012TRA { get; set; }
        public DbSet<N0012VEI> N0012VEI { get; set; }
        public DbSet<N0013MCP> N0013MCP { get; set; }
        public DbSet<N0014REP> N0014REP { get; set; }
        public DbSet<N0018DEP> N0018DEP { get; set; }
        public DbSet<N0018LPD> N0018LPD { get; set; }
        public DbSet<N0019CPR> N0019CPR { get; set; }
        public DbSet<N0019CTE> N0019CTE { get; set; }
        public DbSet<N0020ABR> N0020ABR { get; set; }
        public DbSet<N0023LED> N0023LED { get; set; }
        public DbSet<N0044CCU> N0044CCU { get; set; }
        public DbSet<N0101LOC> N0101LOC { get; set; }
        public DbSet<N0102ESE> N0102ESE { get; set; }
        public DbSet<N0103TUA> N0103TUA { get; set; }
        public DbSet<N0104TPE> N0104TPE { get; set; }
        public DbSet<N0105TLE> N0105TLE { get; set; }
        public DbSet<N0106END> N0106END { get; set; }
        public DbSet<N0107DEN> N0107DEN { get; set; }
        public DbSet<N0108UCA> N0108UCA { get; set; }
        public DbSet<N0109HUA> N0109HUA { get; set; }
        public DbSet<N0109IUA> N0109IUA { get; set; }
        public DbSet<N0109LOT> N0109LOT { get; set; }
        public DbSet<N0109MOV> N0109MOV { get; set; }
        public DbSet<N0109UAR> N0109UAR { get; set; }
        public DbSet<N0110DOC> N0110DOC { get; set; }
        public DbSet<N0110ITD> N0110ITD { get; set; }
        public DbSet<N0110ROM> N0110ROM { get; set; }
        public DbSet<N0111CURModel> N0111CUR { get; set; }
        public DbSet<N0111INRModel> N0111INR { get; set; }
        public DbSet<N0111INVModel> N0111INV { get; set; }
        public DbSet<N0111ITV> N0111ITV { get; set; }
        public DbSet<N0111LOT> N0111LOT { get; set; }
        public DbSet<N0111UAV> N0111UAV { get; set; }
        public DbSet<N0112TAR> N0112TAR { get; set; }
        public DbSet<N0112TPT> N0112TPT { get; set; }
        public DbSet<N0200PER> N0200PER { get; set; }
        public DbSet<N0201SIT> N0201SIT { get; set; }
        public DbSet<N0202APR> N0202APR { get; set; }
        public DbSet<N0202MOT> N0202MOT { get; set; }
        public DbSet<N0202REQ> N0202REQ { get; set; }
        public DbSet<N0202TRA> N0202TRA { get; set; }
        public DbSet<N0203ANX> N0203ANX { get; set; }
        public DbSet<N0203APR> N0203APR { get; set; }
        public DbSet<N0203IPV> N0203IPV { get; set; }
        public DbSet<N0203ITR> N0203ITR { get; set; }
        public DbSet<N0203REG> N0203REG { get; set; }
        public DbSet<N0203TRA> N0203TRA { get; set; }
        public DbSet<N0203UAP> N0203UAP { get; set; }
        public DbSet<N0203OPE> N0203OPE { get; set; }
        public DbSet<N0203UOF> N0203UOF { get; set; }
        public DbSet<N0204MDO> N0204MDO { get; set; }
        public DbSet<N0204AOR> N0204AOR { get; set; }
        public DbSet<N0204ATD> N0204ATD { get; set; }
        public DbSet<N0204MDV> N0204MDV { get; set; }
        public DbSet<N0204ORI> N0204ORI { get; set; }
        public DbSet<N0204PPU> N0204PPU { get; set; }
        public DbSet<N0204AUS> N0204AUS { get; set; }
        public DbSet<N9999MEN> N9999MEN { get; set; }
        public DbSet<N9999SIS> N9999SIS { get; set; }
        public DbSet<N9999UXM> N9999UXM { get; set; }
        public DbSet<N9999USM> N9999USM { get; set; }
        public DbSet<N9999USU> N9999USU { get; set; }
        public DbSet<SYS_CAIXA> SYS_CAIXA { get; set; }
        public DbSet<SYS_CATEND> SYS_CATEND { get; set; }
        public DbSet<SYS_CFGGER> SYS_CFGGER { get; set; }
        public DbSet<SYS_CLASSES> SYS_CLASSES { get; set; }
        public DbSet<SYS_COMSQL> SYS_COMSQL { get; set; }
        public DbSet<SYS_CONCORRENCIA> SYS_CONCORRENCIA { get; set; }
        public DbSet<SYS_CONSULTA> SYS_CONSULTA { get; set; }
        public DbSet<SYS_FICHATECNICA> SYS_FICHATECNICA { get; set; }
        public DbSet<SYS_FICHAXCOMPONENTE> SYS_FICHAXCOMPONENTE { get; set; }
        public DbSet<SYS_HELP> SYS_HELP { get; set; }
        public DbSet<SYS_HELPBODY> SYS_HELPBODY { get; set; }
        public DbSet<SYS_KPI> SYS_KPI { get; set; }
        public DbSet<SYS_LAYOUT> SYS_LAYOUT { get; set; }
        public DbSet<SYS_LOGMSG> SYS_LOGMSG { get; set; }
        public DbSet<SYS_LTELA> SYS_LTELA { get; set; }
        public DbSet<SYS_MAIL> SYS_MAIL { get; set; }
        public DbSet<SYS_REFRULES> SYS_REFRULES { get; set; }
        public DbSet<SYS_REGINI> SYS_REGINI { get; set; }
        public DbSet<SYS_REGRAS> SYS_REGRAS { get; set; }
        public DbSet<SYS_REPORTS> SYS_REPORTS { get; set; }
        public DbSet<SYS_USRCOLUMN> SYS_USRCOLUMN { get; set; }
        public DbSet<SYS_USRCOMPONENT> SYS_USRCOMPONENT { get; set; }
        public DbSet<SYS_USRFORM> SYS_USRFORM { get; set; }
        public DbSet<SYS_USRFORMXTABLE> SYS_USRFORMXTABLE { get; set; }
        public DbSet<SYS_USRLOG> SYS_USRLOG { get; set; }
        public DbSet<SYS_USRON> SYS_USRON { get; set; }
        public DbSet<SYS_USRTABLE> SYS_USRTABLE { get; set; }
        public DbSet<SYS_USRXREL> SYS_USRXREL { get; set; }
        public DbSet<SYS_USUARIO> SYS_USUARIO { get; set; }
        public DbSet<V_SALDONWMS> V_SALDONWMS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new N0000AGEMap());
            modelBuilder.Configurations.Add(new N0000ERRMap());
            modelBuilder.Configurations.Add(new N0000LOGMap());
            modelBuilder.Configurations.Add(new N0000RELMap());
            modelBuilder.Configurations.Add(new N0001EMPMap());
            modelBuilder.Configurations.Add(new N0001EMP_HISMap());
            modelBuilder.Configurations.Add(new N0002FILMap());
            modelBuilder.Configurations.Add(new N0003ARMMap());
            modelBuilder.Configurations.Add(new N0005TNSMap());
            modelBuilder.Configurations.Add(new N0006DERMap());
            modelBuilder.Configurations.Add(new N0006FAMMap());
            modelBuilder.Configurations.Add(new N0006ORIMap());
            modelBuilder.Configurations.Add(new N0006PROMap());
            modelBuilder.Configurations.Add(new N0007UNIMap());
            modelBuilder.Configurations.Add(new N0008CNVMap());
            modelBuilder.Configurations.Add(new N0009ITPMap());
            modelBuilder.Configurations.Add(new N0009TABMap());
            modelBuilder.Configurations.Add(new N0009VLDMap());
            modelBuilder.Configurations.Add(new N0010UCTMap());
            modelBuilder.Configurations.Add(new N0011CLIMap());
            modelBuilder.Configurations.Add(new N0011ENTMap());
            modelBuilder.Configurations.Add(new N0012LVMMap());
            modelBuilder.Configurations.Add(new N0012MOTMap());
            modelBuilder.Configurations.Add(new N0012TRAMap());
            modelBuilder.Configurations.Add(new N0012VEIMap());
            modelBuilder.Configurations.Add(new N0013MCPMap());
            modelBuilder.Configurations.Add(new N0014REPMap());
            modelBuilder.Configurations.Add(new N0018DEPMap());
            modelBuilder.Configurations.Add(new N0018LPDMap());
            modelBuilder.Configurations.Add(new N0019CPRMap());
            modelBuilder.Configurations.Add(new N0019CTEMap());
            modelBuilder.Configurations.Add(new N0020ABRMap());
            modelBuilder.Configurations.Add(new N0023LEDMap());
            modelBuilder.Configurations.Add(new N0044CCUMap());
            modelBuilder.Configurations.Add(new N0101LOCMap());
            modelBuilder.Configurations.Add(new N0102ESEMap());
            modelBuilder.Configurations.Add(new N0103TUAMap());
            modelBuilder.Configurations.Add(new N0104TPEMap());
            modelBuilder.Configurations.Add(new N0105TLEMap());
            modelBuilder.Configurations.Add(new N0106ENDMap());
            modelBuilder.Configurations.Add(new N0107DENMap());
            modelBuilder.Configurations.Add(new N0108UCAMap());
            modelBuilder.Configurations.Add(new N0109HUAMap());
            modelBuilder.Configurations.Add(new N0109IUAMap());
            modelBuilder.Configurations.Add(new N0109LOTMap());
            modelBuilder.Configurations.Add(new N0109MOVMap());
            modelBuilder.Configurations.Add(new N0109UARMap());
            modelBuilder.Configurations.Add(new N0110DOCMap());
            modelBuilder.Configurations.Add(new N0110ITDMap());
            modelBuilder.Configurations.Add(new N0110ROMMap());
            modelBuilder.Configurations.Add(new N0111CURMap());
            modelBuilder.Configurations.Add(new N0111INRMap());
            modelBuilder.Configurations.Add(new N0111INVMap());
            modelBuilder.Configurations.Add(new N0111ITVMap());
            modelBuilder.Configurations.Add(new N0111LOTMap());
            modelBuilder.Configurations.Add(new N0111UAVMap());
            modelBuilder.Configurations.Add(new N0112TARMap());
            modelBuilder.Configurations.Add(new N0112TPTMap());
            modelBuilder.Configurations.Add(new N0200PERMap());
            modelBuilder.Configurations.Add(new N0201SITMap());
            modelBuilder.Configurations.Add(new N0202APRMap());
            modelBuilder.Configurations.Add(new N0202MOTMap());
            modelBuilder.Configurations.Add(new N0202REQMap());
            modelBuilder.Configurations.Add(new N0202TRAMap());
            modelBuilder.Configurations.Add(new N0203ANXMap());
            modelBuilder.Configurations.Add(new N0203APRMap());
            modelBuilder.Configurations.Add(new N0203IPVMap());
            modelBuilder.Configurations.Add(new N0203ITRMap());
            modelBuilder.Configurations.Add(new N0203REGMap());
            modelBuilder.Configurations.Add(new N0203UAPMap());
            modelBuilder.Configurations.Add(new N0203TRAMap());
            modelBuilder.Configurations.Add(new N0203OPEMap());
            modelBuilder.Configurations.Add(new N0203UOFMap());
            modelBuilder.Configurations.Add(new N0204MDOMap());
            modelBuilder.Configurations.Add(new N0204AORMap());
            modelBuilder.Configurations.Add(new N0204ATDMap());
            modelBuilder.Configurations.Add(new N0204MDVMap());
            modelBuilder.Configurations.Add(new N0204ORIMap());
            modelBuilder.Configurations.Add(new N0204PPUMap());
            modelBuilder.Configurations.Add(new N0204AUSMap());
            modelBuilder.Configurations.Add(new N9999MENMap());
            modelBuilder.Configurations.Add(new N9999SISMap());
            modelBuilder.Configurations.Add(new N9999UXMMap());
            modelBuilder.Configurations.Add(new N9999USMMap());
            modelBuilder.Configurations.Add(new N9999USUMap());
            modelBuilder.Configurations.Add(new SYS_CAIXAMap());
            modelBuilder.Configurations.Add(new SYS_CATENDMap());
            modelBuilder.Configurations.Add(new SYS_CFGGERMap());
            modelBuilder.Configurations.Add(new SYS_CLASSESMap());
            modelBuilder.Configurations.Add(new SYS_COMSQLMap());
            modelBuilder.Configurations.Add(new SYS_CONCORRENCIAMap());
            modelBuilder.Configurations.Add(new SYS_CONSULTAMap());
            modelBuilder.Configurations.Add(new SYS_FICHATECNICAMap());
            modelBuilder.Configurations.Add(new SYS_FICHAXCOMPONENTEMap());
            modelBuilder.Configurations.Add(new SYS_HELPMap());
            modelBuilder.Configurations.Add(new SYS_HELPBODYMap());
            modelBuilder.Configurations.Add(new SYS_KPIMap());
            modelBuilder.Configurations.Add(new SYS_LAYOUTMap());
            modelBuilder.Configurations.Add(new SYS_LOGMSGMap());
            modelBuilder.Configurations.Add(new SYS_LTELAMap());
            modelBuilder.Configurations.Add(new SYS_MAILMap());
            modelBuilder.Configurations.Add(new SYS_REFRULESMap());
            modelBuilder.Configurations.Add(new SYS_REGINIMap());
            modelBuilder.Configurations.Add(new SYS_REGRASMap());
            modelBuilder.Configurations.Add(new SYS_REPORTSMap());
            modelBuilder.Configurations.Add(new SYS_USRCOLUMNMap());
            modelBuilder.Configurations.Add(new SYS_USRCOMPONENTMap());
            modelBuilder.Configurations.Add(new SYS_USRFORMMap());
            modelBuilder.Configurations.Add(new SYS_USRFORMXTABLEMap());
            modelBuilder.Configurations.Add(new SYS_USRLOGMap());
            modelBuilder.Configurations.Add(new SYS_USRONMap());
            modelBuilder.Configurations.Add(new SYS_USRTABLEMap());
            modelBuilder.Configurations.Add(new SYS_USRXRELMap());
            modelBuilder.Configurations.Add(new SYS_USUARIOMap());
            modelBuilder.Configurations.Add(new V_SALDONWMSMap());
        }
    }
}
