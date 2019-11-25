using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;


namespace elconnect
{
    class ELmain
    {
        EyelinkWindow elW;
        SREYELINKLib.EyeLink el = null;
        SREYELINKLib.EyeLinkUtil elutil = null;

        public void open_elW()
        {
            try
            {
                elW = new EyelinkWindow();
                elW.Show();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void generate_el_file(string eyelink_fileName)
        {
            el = new SREYELINKLib.EyeLink();
            el.open("100.1.1.1", 0);
            el.openDataFile(eyelink_fileName);
            el.sendCommand("file_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,MESSAGE,BUTTON");
            el.sendCommand("file_sample_data = LEFT,RIGHT,GAZE,AREA,GAZERES,STATUS");
            el.sendCommand("link_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,BUTTON");
            el.sendCommand("link_sample_data = LEFT,RIGHT,GAZE,GAZERES,AREA,STATUS");
            el.sendCommand("screen_pixel_coords=0,0," + elW.Width + "," + elW.Height);
            el.sendMessage(eyelink_fileName);
        }

        public void calib()
        {
            try
            {
            // send handle to sdk
            elutil = new SREYELINKLib.EyeLinkUtil();
                SREYELINKLib.ELGDICal cal = elutil.getGDICal();
                // calibration
                cal.setCalibrationColors(ConvertColorToColorRef(elW.ForeColor), ConvertColorToColorRef(elW.BackColor));
            cal.setCalibrationWindow(elW.Handle.ToInt32());
                //cal.setCalibrationTargetSize(6, 3);
                cal.enableKeyCollection(true);
            el.doTrackerSetup();
            cal.enableKeyCollection(false);
            // drift correction
            cal.enableKeyCollection(true);
            el.doDriftCorrect((short)(elW.Width / 2), (short)(elW.Height / 2), true, true);
            cal.enableKeyCollection(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static UInt32 ConvertColorToColorRef(Color color)
        {
            UInt32 blue = color.R;
            UInt32 green = color.G;
            UInt32 red = color.B;
            UInt32 colorRef = red << 16 | green << 8 | blue;
            return colorRef;
        }
        
        public void trial_ith(int ith)
        {
            try
            {
                el.sendCommand(String.Format("record_status_message 'TRIAL {0}'", ith));
                el.sendMessage(String.Format("TRIALID {0}", ith));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void add_label(string label)
        {
            try
            {
                el.sendMessage(String.Format(label));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void startRecord()
        {
            try
            {
                el.startRecording(true, true, true, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void stopRecord(string eyelink_fileName)
        {
            try
            {
                el.sendCommand("close_data_file");// close edf file on Host
                el.receiveDataFile(eyelink_fileName, eyelink_fileName);
                el.setOfflineMode(); //set offline mode for speedier transfer of EDF file  Host 
                el.stopRecording();
                el.close();
                el = null;
                elutil = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



    }


}