using ImageProcessor;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using ImageProcessor.Imaging.Filters.Photo;
using LibSVMsharp;
using LibSVMsharp.Extensions;
using LibSVMsharp.Helpers;
using System;
using System.Drawing;
using System.IO;

namespace CozyHammer.MnistSvm
{
    class Program
    {
        const string MnistDataPath = @"d:\code\dotnet\b9\mnist\";

        static void Main(string[] args)
        {
            // 1、mnist数据转为libsvm的输入格式
            //TransData("t10k");
            //TransData("train");

            // 2、训练得到模型
            //Train("t10k");

            // 3、验证识别准确率
            //Test("t1k");
            //Test("t10k");
            //Test("train");

            // 4、图片转向量
            // 先转灰度图、然后缩放到28*28
            TestImage("1");
            TestImage("1_2");
            TestImage("2");
            TestImage("3");
            TestImage("9_1");
            TestImage("9_2");

            TestOne("hehe");


            Console.WriteLine("\n\nPress any key to quit...");
            Console.ReadLine();
        }

        private static void TransData(string prefix)
        {
            MnistDataLabelReader lr = new MnistDataLabelReader();
            bool b1 = lr.ParseData(MnistDataPath + prefix + "-labels.idx1-ubyte");

            MnistDataImageReader ir = new MnistDataImageReader();
            bool b2 = ir.ParseData(MnistDataPath + prefix + "-images.idx3-ubyte");

            if (b1 && b2 && lr.ItemCount == ir.ItemCount)
            {
                MnistDataWriter dw = new MnistDataWriter();
                dw.Save(lr, ir, MnistDataPath + prefix + ".txt");
            }
        }

        private static void Train(string prefix)
        {
            SVMProblem trainingSet = SVMProblemHelper.Load(MnistDataPath + prefix + ".txt");
            trainingSet = trainingSet.Normalize(SVMNormType.L2);

            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.RBF;
            parameter.C = 1;
            parameter.Gamma = 1;

            double[] crossValidationResults;
            int nFold = 5;
            trainingSet.CrossValidation(parameter, nFold, out crossValidationResults);
            double crossValidationAccuracy = trainingSet.EvaluateClassificationProblem(crossValidationResults);
            Console.WriteLine("\n\nCross validation accuracy: " + crossValidationAccuracy);

            SVMModel model = trainingSet.Train(parameter);
            SVM.SaveModel(model, MnistDataPath + "model.txt");
            Console.WriteLine("\n\nModel ok!");
        }

        private static void Test(string prefix)
        {
            SVMModel model = SVM.LoadModel(MnistDataPath + "model.txt");
            SVMProblem testSet = SVMProblemHelper.Load(MnistDataPath + prefix + ".txt");
            testSet = testSet.Normalize(SVMNormType.L2);
            double[] testResults = testSet.Predict(model);
            int[,] confusionMatrix;
            double testAccuracy = testSet.EvaluateClassificationProblem(testResults, model.Labels, out confusionMatrix);
            Console.WriteLine("\nTest accuracy: " + testAccuracy);
        }

        private static void TestImage(string prefix)
        {
            var fac = new ImageFactory();
            fac.Load(MnistDataPath + prefix + ".png")
               .DetectEdges(new Laplacian3X3EdgeFilter())
               .Resize(new Size(28, 28))
               .BackgroundColor(Color.Black)
               .Save(MnistDataPath + prefix + ".bmp")
               .Dispose();

            Bitmap bm = new Bitmap(MnistDataPath + prefix + ".bmp");
            FileStream fs = new FileStream(MnistDataPath + prefix + ".txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string line = "0 ";
            for (var j = 0; j < bm.Width * bm.Height; ++j)
            {
                line += j.ToString() + ":" + (bm.GetPixel(j % bm.Width, j / bm.Width).R == 0 ? 0 : 1) + " ";
            }
            sw.WriteLine(line);
            sw.Flush();
            sw.Close();
            fs.Close();

            TestOne(prefix);
        }

        private static void TestOne(string prefix)
        {
            SVMModel model = SVM.LoadModel(MnistDataPath + "model.txt");
            SVMProblem testSet = SVMProblemHelper.Load(MnistDataPath + prefix + ".txt");
            testSet = testSet.Normalize(SVMNormType.L2);
            double[] testResults = testSet.Predict(model);
            Console.WriteLine("\nTest result: " + testResults[0].ToString());
        }
    }
}
