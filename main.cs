using ExifLib;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;


public class ImageInformationClass
{
    public ImageInformationClass()
    {
        this.Lat = 0;
        this.Long = 0;
    }
    public double Lat { get; set; }
    public double Long { get; set; }
    public string Direction_Information { get; set; }
    public double Direction { get; set; }
    public DateTime EXIF_Datetime { get; set; }

    public double GPSImageDirection { get; set; }

}
public class GetDataFromEXIF
{
    public ImageInformationClass get_all_data_from_EXIF(string filename)
    {
        ImageInformationClass return_result = new ImageInformationClass();
        double lat = 0, lon = 0;
        string Direction_Information;
        double Direction;
        DateTime EXIF_datetime;
        string[] results = new string[5];
        try
        {
            using (ExifReader reader = new ExifReader(filename))
            {
                // Extract the tag data using the ExifTags enumeration

                double[] lat_double = new double[3];
                if (reader.GetTagValue<double[]>(ExifTags.GPSLatitude,
                                                out lat_double))
                {
                    lat = lat_double[0] + lat_double[1] / 60 + lat_double[2] / 3600;
                }

                double[] lon_double = new double[3];
                if (reader.GetTagValue<double[]>(ExifTags.GPSLongitude,
                                                out lon_double))
                {
                    lon = lon_double[0] + lon_double[1] / 60 + lon_double[2] / 3600;
                }

                if (reader.GetTagValue<string>(ExifTags.GPSImgDirectionRef,
                                                out Direction_Information))
                {
                }

                if (reader.GetTagValue<double>(ExifTags.GPSImgDirection,
                                                out Direction))
                {
                }

                if (reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal,
                                                out EXIF_datetime))
                {
                }

            }
            results[0] = lat.ToString();
            results[1] = lon.ToString();
            results[2] = Direction_Information;
            results[3] = Direction.ToString();
            results[4] = EXIF_datetime.ToString();

            return_result.Lat = lat;
            return_result.Long = lon;
            return_result.Direction_Information = Direction_Information;
            return_result.Direction = Direction;
            return_result.EXIF_Datetime = EXIF_datetime;
            return_result.GPSImageDirection = Direction;
        }
        catch
        {

        }
        return return_result;
    }
}
