using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Model;
using ModelCasc.operation;

namespace ModelCasc.catalog.personal
{
    public class PersonalCtrl
    {
        //public static void PersonalRegistro(Personal o)
        //{
        //    try
        //    {
        //        PersonalMng oPMng = new PersonalMng() { O_Personal = o };
        //        oPMng.selByFolio();
        //        Personal_registroMng oMng = new Personal_registroMng()
        //        {
        //            O_Personal_registro = new Personal_registro()
        //            {
        //                Id_personal = o.Id,
        //                Id_bodega = o.Id_bodega
        //            }
        //        };
        //        oMng.add();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public static List<Personal_rol> PersonalRolFill()
        {
            List<Personal_rol> lst = new List<Personal_rol>();
            try
            {
                Personal_rolMng oMng = new Personal_rolMng();
                oMng.fillLst();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }

            return lst;
        }

        public static List<Personal_empresa> PersonalEmpresaFill()
        {
            List<Personal_empresa> lst = new List<Personal_empresa>();
            try
            {
                Personal_empresaMng oMng = new Personal_empresaMng();
                oMng.fillLst();
                lst = oMng.Lst;
            }
            catch 
            {
                throw;
            }

            return lst;
        }

        public static Personal_qr PersonalQRGetByIdPersona(int id_persona)
        {
            Personal_qr oPQr = new Personal_qr() { Id_personal = id_persona };
            try
            {
                Personal_qrMng oPQrMng = new Personal_qrMng();
                oPQrMng.O_Personal_qr = oPQr;
                oPQrMng.selByIdPersonal();
            }
            catch
            {
                
                throw;
            }
            return oPQr;
        }

        private static Personal_qr PersonalQRAdd(IDbTransaction trans, Personal o)
        {
            Personal_qr oPQr = new Personal_qr() { Id_personal = o.Id, Idf = o.PQr.Idf };
            try
            {
                Personal_qrMng oPQrMng = new Personal_qrMng();
                oPQrMng.O_Personal_qr = oPQr;
                oPQrMng.add(trans);

                Personal_qr_pivoteMng oPQRPivMng = new Personal_qr_pivoteMng();
                Personal_qr_pivote oPQRPiv = new Personal_qr_pivote() { Idf = o.PQr.Idf };
                oPQRPivMng.O_Personal_qr_pivote = oPQRPiv;
                oPQRPivMng.dltByIdf(trans);
            }
            catch
            {
                throw;
            }
            return oPQr;
        }

        public static Personal PersonalGet(int Id)
        {
            Personal o = new Personal();
            try
            {
                o.Id = Id;
                PersonalMng oMng = new PersonalMng();
                oMng.O_Personal = o;
                oMng.selById();

                Personal_archivosMng oPAMng = new Personal_archivosMng();
                Personal_archivos oPA = new Personal_archivos() { Id_personal = Id };
                oPAMng.O_Personal_archivos = oPA;
                oPAMng.selByIdPersonal();
                o.lstArchivos = oPAMng.Lst;

                o.PQr = PersonalQRGetByIdPersona(Id);
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static void PersonalTempDataByUser(int id_usuario)
        {
            try
            {
                PersonalFotoDltByUser(id_usuario);
                PersonalQrPivoteDltByUser(id_usuario);
            }
            catch
            {
                throw;
            }
        }

        private static void PersonalFotoDltByUser(int id_usuario)
        {
            try
            {
                Personal_fotoMng oMng = new Personal_fotoMng();
                Personal_foto o = new Personal_foto() { Id_usuario = id_usuario };
                oMng.O_Personal_foto = o;
                oMng.dltByUser();
            }
            catch
            {
                
                throw;
            }
        }

        private static void PersonalQrPivoteDltByUser(int id_usuario)
        {
            try
            {
                Personal_qr_pivoteMng oPqrPMng = new Personal_qr_pivoteMng();
                Personal_qr_pivote oPQrPiv = new Personal_qr_pivote() { Id_usuario = id_usuario };
                oPqrPMng.O_Personal_qr_pivote = oPQrPiv;
                oPqrPMng.dltByUser();
            }
            catch
            {
                throw;
            }
        }

        public static string PersonalQrPivoteGetFolio(int id_usuario)
        {
            string folio = string.Empty;
            try
            {
                Personal_qr_pivoteMng oPqrPMng = new Personal_qr_pivoteMng();
                Personal_qr_pivote oPQrPiv = new Personal_qr_pivote() { Id_usuario = id_usuario };
                oPqrPMng.O_Personal_qr_pivote = oPQrPiv;
                oPqrPMng.fillLstByIdUsuario();
                if (oPqrPMng.Lst.Count > 0)
                    folio = oPqrPMng.Lst.First().Idf;
            }
            catch
            {
                throw;
            }
            return folio;
        }

        public static void PersonalAdd(Personal o, int id_usuario)
        {
            IDbTransaction trans = null;
            try
            {
                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();
                PersonalMng oMng = new PersonalMng();
                oMng.O_Personal = o;
                oMng.add(trans);

                o.PQr = PersonalQRAdd(trans, o);

                PersonalAddFiles(o.lstArchivos, o, trans);
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void PersonalUdt(Personal o, int id_usuario)
        {
            IDbTransaction trans = null;
            try
            {
                Personal oAnt = PersonalCtrl.PersonalGet(o.Id);

                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();
                PersonalMng oMng = new PersonalMng();
                oMng.O_Personal = o;
                oMng.udt(trans);

                Personal_qrMng oPQRMng = new Personal_qrMng();
                Personal_qr oPQR = new Personal_qr() { Id_personal = o.Id };
                oPQRMng.O_Personal_qr = oPQR;
                oPQRMng.dltByIdPersonal(trans);

                o.PQr = PersonalQRAdd(trans, o);

                if (string.Compare(oAnt.PQr.Idf, o.PQr.Idf) != 0)
                    PersonalUdtFiles(oAnt, o);
                //else
                //    PersonalAddFiles(o.lstArchivos, o, trans);

                if (o.lstArchivos.Count > 0)
                {
                    Personal_archivosMng oPAMng = new Personal_archivosMng();
                    Personal_archivos oPA = new Personal_archivos() { Id_personal = o.Id };
                    oPAMng.O_Personal_archivos = oPA;
                    oPAMng.dltByIdPersonal(trans);

                    PersonalAddFiles(o.lstArchivos, o, trans);
                }
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        public static void PersonalChangeStatus(Personal o, bool status)
        {
            try
            {
                PersonalMng oMng = new PersonalMng();
                oMng.O_Personal = o;
                if (status)
                    oMng.dlt();
                else
                    oMng.reactive();
            }
            catch
            {
                throw;
            }
        }

        public static List<Personal> PersonalFillEvenInactive()
        {
            List<Personal> lst = new List<Personal>();
            try
            {
                PersonalMng oMng = new PersonalMng();
                oMng.fillEvenInactive();
                lst = oMng.Lst;

                Personal_empresaMng oPEmpMng = new Personal_empresaMng();
                foreach (Personal itemP in lst)
                {
                    Personal_empresa oPE = new Personal_empresa() { Id = itemP.Id_personal_empresa };
                    oPEmpMng.O_Personal_empresa = oPE;
                    oPEmpMng.selById();
                    itemP.PerEmp = oPE;
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static Personal PersonalUltimoRegistroPorBodega(int id_bodega)
        {
            Personal oP = new Personal();
            try
            {
                Personal_registro oPR = new Personal_registro() { Id_bodega = id_bodega };
                Personal_registroMng oPRMng = new Personal_registroMng() { O_Personal_registro = oPR };
                oPRMng.selUltPorBodega();

                Personal_qr oPQr = new Personal_qr() { Id_personal = oPR.Id_personal };
                Personal_qrMng oPQrMng = new Personal_qrMng() { O_Personal_qr = oPQr };
                oPQrMng.selByIdPersonal();
                oP.PQr = oPQr;

                oP.Id = oPQr.Id_personal;
                PersonalMng oPMng = new PersonalMng() { O_Personal = oP };
                oPMng.selById();
                oP.PerReg = oPR;
            }
            catch
            {
                throw;
            }
            return oP;
        }

        private static void PersonalUdtFiles(Personal ant, Personal act)
        {
            try
            {
                Copy(Path.Combine(act.RutaFiles, ant.PQr.Idf), Path.Combine(act.RutaFiles, act.PQr.Idf));
            }
            catch
            {
                throw;
            }
        }

        private static void PersonalAddFiles(List<Personal_archivos> lst, Personal personal, IDbTransaction trans)
        {
            Personal_archivosMng oMng = new Personal_archivosMng();
            try
            {
                Personal_archivo_tipoMng oPATMng = new Personal_archivo_tipoMng();
                oPATMng.fillLst();

                foreach (Personal_archivos o in lst)
                {

                    Directory.CreateDirectory(Path.Combine(o.Ruta, personal.PQr.Idf));

                    string tipo = oPATMng.Lst.Find(p => p.Id == o.Id_archivo_tipo).Tipo;
                    o.Id_personal = personal.Id;
                    o.Ruta = Path.Combine(o.Ruta, personal.PQr.Idf + @"\", tipo + ".jpg");
                    oMng.O_Personal_archivos = o;

                    if (File.Exists(o.Ruta))
                    {
                        File.Delete(o.Ruta);
                    }

                    FileStream fs = new FileStream(o.Ruta, FileMode.CreateNew, FileAccess.ReadWrite);
                    o.stream.Position = 0;
                    o.stream.CopyTo(fs);
                    fs.Close();
                    fs.Dispose();
                    oMng.add(trans);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void Copy(string sourceDir, string targetDir)
        {
                Directory.CreateDirectory(targetDir);

                foreach (var file in Directory.GetFiles(sourceDir))
                    File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)), true);

                foreach (var directory in Directory.GetDirectories(sourceDir))
                    Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        public static Personal_qr_pivote PersonalQrPivoteAdd(Personal_qr_pivote o)
        {
            try
            {
                Personal_qr_pivoteMng oMng = new Personal_qr_pivoteMng() { O_Personal_qr_pivote = o };
                oMng.add();
            }
            catch 
            {
                throw;
            }
            return o;
        }

        public static Personal_foto PersonalFotoUdt(int id_usuario)
        {
            Personal_foto o = new Personal_foto() { Id_usuario = id_usuario};
            try
            {
                Personal_fotoMng oMng = new Personal_fotoMng();
                oMng.O_Personal_foto = o;
                oMng.selByIdUsuario();
            }
            catch
            {
                
                throw;
            }
            return o;
        }

        public static Personal_foto PersonalFotoAdd(Personal_foto o, string path)
        {
            Personal_fotoMng oMng = new Personal_fotoMng() { O_Personal_foto = o };
            IDbTransaction trans = null;
            try
            {
                //Comienza la transanccion
                trans = GenericDataAccess.BeginTransaction();
                string provname = DateTime.Now.ToString("hhmmssffffff");
                CommonCtrl.AddImgToDirectory(path, provname, o.Foto);
                o.Foto = provname;
                oMng.add(trans);
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return o;
        }

        public static Personal_qr PersonalRegistro(Personal_qr o)
        {
            bool cumpleReglas = false;
            try
            {
                Personal_qrMng oPQrMng = new Personal_qrMng() { O_Personal_qr = o };
                oPQrMng.selByIdf();

                Personal_registro oPreg = new Personal_registro() { Id_personal = o.Id_personal, Id_bodega = o.Id_bodega };
                Personal_registroMng oMng = new Personal_registroMng() { O_Personal_registro = oPreg };
                oMng.selByIdPersona();

                Personal oPer = new Personal() { Id = o.Id_personal };
                PersonalMng oPerMng = new PersonalMng() { O_Personal = oPer };
                oPerMng.selById();
                o.PPersonal = oPer;

                Personal_regla_rol oPerRegRol = new Personal_regla_rol() { Id_personal_rol = oPer.Id_personal_rol };
                Personal_regla_rolMng oPerRegRolMng = new Personal_regla_rolMng() { O_Personal_regla_rol = oPerRegRol };
                oPerRegRolMng.selByIdPersonalRol();

                foreach (Personal_regla_rol itemPRR in oPerRegRolMng.Lst)
                {
                    Personal_regla oPRegla = new Personal_regla() { Id = itemPRR.Id_personal_regla };
                    Personal_reglaMng oPregMng = new Personal_reglaMng() { O_Personal_regla = oPRegla };
                    oPregMng.selById();

                    TimeSpan ts = DateTime.Now - oPreg.Fecha_hora;
                    int valor = Convert.ToInt32(oPRegla.Valor);
                    if (ts.TotalMinutes > valor)
                        cumpleReglas = true;
                    else
                        o.Mensaje = oPRegla.Mensaje.Replace("{{valor}}", Math.Round(Math.Abs(ts.TotalMinutes - valor), 0).ToString());
                }

                if (!oPer.IsActive)
                {
                    o.Mensaje = "El personal está dado de baja y no puede acceder a la sede, favor de verificar con el área de Recursos Humanos";
                }
                else if (oPer.Boletinado)
                {
                    o.Mensaje = "El personal está boletinado y no puede acceder a ninguna sede, favor de verificar con el área de Recursos Humanos";
                }
                else if (cumpleReglas)
                {
                    o.Mensaje = "Registro Exitoso";
                    oMng.add();
                    o.PPerReg = oPreg;
                }
            }
            catch
            {
                throw;
            }
            return o;
        }
    }
}
