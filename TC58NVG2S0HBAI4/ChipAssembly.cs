using NAND_Prog;
using System;
using System.ComponentModel.Composition;

namespace TC58NVG2S0HBAI4
{
    /*
     use the design :

      # region
         <some code>
      # endregion

    for automatically include <some code> in the READMY.md file in the repository
    */
    public class ChipPrototype_v1 : ChipPrototype
    {
        public int EccBits;
    }

    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype_v1();



        #region Chip parameters

        //--------------------Vendor Specific Pin configuration---------------------------

        //  VSP1(38pin) - NC    
        //  VSP2(35pin) - NC
        //  VSP3(20pin) - NC

        ChipAssembly()
        {
            myChip.devManuf = "TOSHIBA";
            myChip.name = "TC58NVG2S0HBAI4";
            myChip.chipID = "98DC902676";      // device ID - 98h DCh 90h 26h 76h

            myChip.width = Organization.x8;    // chip width - 8 bit
            myChip.bytesPP = 4096;             // page size - 4096 byte (4Kb)
            myChip.spareBytesPP = 256;          // size Spare Area - 256 byte
            myChip.pagesPB = 64;               // the number of pages per block - 64 
            myChip.bloksPLUN = 2048;           // number of blocks in CE - 1024
            myChip.LUNs = 1;                   // the amount of CE in the chip
            myChip.colAdrCycles = 2;           // cycles for column addressing
            myChip.rowAdrCycles = 3;           // cycles for row addressing 
            myChip.vcc = Vcc.v3_3;             // supply voltage
            (myChip as ChipPrototype_v1).EccBits = 1;                // required Ecc bits for each 512 bytes

            #endregion


            #region Chip operations

            //------- Add chip operations   https://github.com/JuliProg/Wiki#command-set---------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

            #endregion



            #region Chip registers (optional)

            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(                   // https://github.com/JuliProg/Wiki/wiki/StatusRegister
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").   
                UseAsStatusRegister();



            myChip.registers.Add(                  // https://github.com/JuliProg/Wiki/wiki/ID-Register
                "Id Register").
                Size(5).
                Operations("ReadId_90h");               
                //Interpretation(ID_interpreting);          

            #endregion


        }

     
       
    }

}
