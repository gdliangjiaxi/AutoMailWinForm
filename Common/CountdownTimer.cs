using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
   
    /// </summary>
    public partial class CountdownTimer : UserControl
    {

        //控件的最小宽度
        private int controlMinWidth;

        //控件的最小高度
        private int controlMinHeigh;

        //计时器对象
        private Timer timer;

        //倒计时字符数组
        private char[] charsTime;
        //倒计时时间的复制变量
        private int tempTime;

        //倒计时时间的复制变量str
        private string strTempTime;

        // 防止控件重复加载 
        private bool isLoaded = false;


        public CountdownTimer()
        {
            InitializeComponent();
            this.DoubleBuffered = true;  // 启用双缓冲

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                                  ControlStyles.UserPaint |
                                  ControlStyles.OptimizedDoubleBuffer, true);

        }

      
        private int timeCountDown=0 ;
        [Category("自定义属性")]
        [Description("设置倒计时的时间,单位秒")]
        public int TimeCountDown
        {
            get { return timeCountDown; }
            set
            {
                timeCountDown = value > 999999 ? 999999 : value; //最大277.7775 小时
                Invalidate();
                //重置计时器
                StartCountdownTimer();
            }
        }

        private int timeRectangleUnit = 6;
        [Category("自定义属性")]
        [Description("绘制倒计时矩形的基本单位")]
        public int TimeRectangleUnit
        {
            get { return timeRectangleUnit; }
            set { timeRectangleUnit = value;Invalidate(); }
        }


        private int timePointX = 20;

        [Category("自定义属性")]
        [Description("绘制倒计时的起始坐标X")]
        public int TimePointX
        {
            get { return timePointX; }
            set { timePointX = value; Invalidate(); }
        }

        private int timePointY = 20;

        [Category("自定义属性")]
        [Description("绘制倒计时的起始坐标Y")]
        public int TimePointY
        {
            get { return timePointY; }
            set { timePointY = value; Invalidate(); }
        }


        private int timeEveryIntervalX = 5;

        [Category("自定义属性")]
        [Description("绘制倒计时的每个八字间隔距离")]
        public int TimeEveryIntervalX
        {
            get { return timeEveryIntervalX; }
            set { timeEveryIntervalX = value; Invalidate(); }
        }


        private Color timeBackGroundColor = Color.Silver;

        [Category("自定义属性")]
        [Description("倒计时矩形的背景颜色")]
        public Color TimeBackGroundColor
        {
            get { return timeBackGroundColor; }
            set { timeBackGroundColor = value; Invalidate(); }
        }

        private Color timeNumColor = Color.Red;

        [Category("自定义属性")]
        [Description("倒计时的数字颜色")]
        public Color TimeNumColor
        {
            get { return timeNumColor; }
            set { timeNumColor = value; Invalidate(); }
        }


        private bool timeCountDownStart = true;

        [Category("自定义属性")]
        [Description("倒计时的数字颜色")]
        public bool TimeCountDownStart
        {
            get { return timeCountDownStart; }
            set { timeCountDownStart = value; Invalidate(); }
        }


        /// <summary>
        /// 绘制倒计时矩形,一共有13个矩形组成一个八字,每个矩形的宽度和高度都是TimeRectangleWidthUnit
        /// </summary>
        private (Rectangle[], Rectangle[]) DrawTimeRectangle()
        {


            #region 绘制88:88:88的矩形背景
            Rectangle[] rectangles = new Rectangle[82];
            List<Rectangle> listCountDown = new List<Rectangle>();

            // 88:88:88
            for (int i = 0; i < 8; i++)
            {

                //冒号会占4个位置
                int j = 0;
                int k = 0;
                if (2 < i && i < 6)
                {
                    j = 11;  //第二组8需要减去13-2个位置,因为一组冒号顶替了一个8字
                    k = 1;   //用于绘制完冒号后的第一个八字间隔,因为一组冒号只用到了一个TimeRectangleWidthUnit，需要减掉2个TimeRectangleWidthUnit          


                }
                else if (i >= 6)
                {
                    j = 22;//第三组8需要减去13-2+13-2个位置,因为两组冒号顶替了两个8字
                    k = 2;
                }

                //开始的矩形位置
                int startPointX  = TimePointX + i * (3 * timeRectangleUnit + TimeEveryIntervalX)-k*(2* timeRectangleUnit);

        

                if (i==2) //绘制冒号 ，一共四个
                {
                    rectangles[i*13] = new Rectangle(startPointX, TimePointY+ timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);

                    rectangles[i * 13+1] = new Rectangle(startPointX, TimePointY + 3* timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                }

                else if (i==5)//绘制冒号 ，一共四个
                {
                    rectangles[(i-1)*13+2] = new Rectangle(startPointX, TimePointY + timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);

                    rectangles[(i - 1) * 13 + 3] = new Rectangle(startPointX, TimePointY + 3 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                }
                else
                {
                   

                    //八字起始矩形
                    rectangles[0 + i * 13 - j] = new Rectangle(startPointX, TimePointY, timeRectangleUnit, timeRectangleUnit);
                    //八字的最上边横线
                    rectangles[1 + i * 13 - j] = new Rectangle(startPointX + timeRectangleUnit, TimePointY, timeRectangleUnit, timeRectangleUnit);
                    rectangles[2 + i * 13 - j] = new Rectangle(startPointX + 2 * timeRectangleUnit, TimePointY, timeRectangleUnit, timeRectangleUnit);
                    //八字最左边竖线
                    rectangles[3 + i * 13 - j] = new Rectangle(startPointX, TimePointY + timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[4 + i * 13 - j] = new Rectangle(startPointX, TimePointY + 2 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[5 + i * 13 - j] = new Rectangle(startPointX, TimePointY + 3 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[6 + i * 13 - j] = new Rectangle(startPointX, TimePointY + 4 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    //TimeRectangleUnit
                    rectangles[7 + i * 13 - j] = new Rectangle(startPointX + 2 * timeRectangleUnit, TimePointY + timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[8 + i * 13 - j] = new Rectangle(startPointX + 2 * timeRectangleUnit, TimePointY + 2 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[9 + i * 13 - j] = new Rectangle(startPointX + 2 * timeRectangleUnit, TimePointY + 3 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[10 + i * 13 - j] = new Rectangle(startPointX + 2 * timeRectangleUnit, TimePointY + 4 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    //八字中间和最下面两个矩形
                    rectangles[11 + i * 13 - j] = new Rectangle(startPointX + timeRectangleUnit, TimePointY + 2 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                    rectangles[12 + i * 13 - j] = new Rectangle(startPointX + timeRectangleUnit, TimePointY + 4 * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                }
                #endregion


            }


            #region 若启动则倒计时开始
            if (TimeCountDownStart&& charsTime!=null)
            {
                int k = 0;

                int? NotEqualZeroIndex=null;



                for (int j= 0; j< charsTime.Length; j++)
                {

                    //如果是一对冒号则需要加多一个矩形单位和两个间隔
                    if (1<j&&j<4)
                    {
                        k = timeRectangleUnit + TimeEveryIntervalX;
                    }
                    else if (j>=4)
                    {
                        k= 2*(timeRectangleUnit + TimeEveryIntervalX);
                    }
                    int startPointX=TimePointX +j * (3 * timeRectangleUnit + TimeEveryIntervalX) + k;
                    char num = charsTime[j];
                    if (num == '0')
                    {
                        //判断是否为不是0数字后的索引或者计时器已结束，需要绘制0
                        if ((NotEqualZeroIndex!=null&&j> NotEqualZeroIndex) || charsTime.All(c => c == '0'))//不能用temptime=0 因为Invalidate() 是一个同步调用，它只是请求控件重绘，并不会阻塞当前线程直到绘制完成。因此，tempTime-- 会在 Invalidate() 调用后立即执行
                        {
                            listCountDown.AddRange(DrawNum0(startPointX));
                        }
                        continue;
                      
                    }
                    else
                    {
                        if (NotEqualZeroIndex==null)//找到第一个不为0的字符索引,后面的0都需要被绘制
                        {
                            NotEqualZeroIndex = j;
                        }
                      
                        switch (num)
                        {
                            case '1':
                                listCountDown.AddRange(DrawNum1(startPointX));
                                break;
                            case '2':
                                listCountDown.AddRange(DrawNum2(startPointX));
                                break;
                            case '3':
                                listCountDown.AddRange(DrawNum3(startPointX));
                                break;
                            case '4':
                                listCountDown.AddRange(DrawNum4(startPointX));
                                break;
                            case '5':
                                listCountDown.AddRange(DrawNum5(startPointX));
                                break;
                            case '6':
                                listCountDown.AddRange(DrawNum6(startPointX));
                                break;
                            case '7':
                                listCountDown.AddRange(DrawNum7(startPointX));
                                break;
                            case '8':
                                listCountDown.AddRange(DrawNum8(startPointX));
                                break;
                            case '9':
                                listCountDown.AddRange(DrawNum9(startPointX));
                                break;
                            default:
                                break;
                        }


                    }
                    
                  
                }
                //绘制冒号
                if (NotEqualZeroIndex != null)
                {
                    switch (NotEqualZeroIndex)
                    {
                        case 0:
                        case 1:
                            listCountDown.AddRange(DrawColon(TimePointX + 6 * timeRectangleUnit + 2 * timeEveryIntervalX));
                            listCountDown.AddRange(DrawColon(TimePointX + 13 * timeRectangleUnit + 5 * timeEveryIntervalX));
                            break;
                        case 2:
                        case 3:
                            listCountDown.AddRange(DrawColon(TimePointX + 13 * timeRectangleUnit + 5 * timeEveryIntervalX));
                            break;
                        default:
                            break;

                    }


                }
                //全为0也要绘制冒号
                if (charsTime.All(c => c == '0'))
                {
                    listCountDown.AddRange(DrawColon(TimePointX + 6 * timeRectangleUnit + 2 * timeEveryIntervalX));
                    listCountDown.AddRange(DrawColon(TimePointX + 13 * timeRectangleUnit + 5 * timeEveryIntervalX));
                }

            }

            #endregion

            return (rectangles, listCountDown.ToArray());

        }


        /// <summary>
        /// 绘制
        /// </summary>
        /// 触发条件:控件或窗体首次显示时
        ///         控件或窗体大小调整时
        ///         控件或窗体从被遮挡状态恢复时
        ///         程序显式请求重绘时调用 Invalidate() 或 Refresh() 方法，可以强制控件或窗体重绘
        ///         窗口区域失效时：比如由于其他窗口移动导致当前窗口的部分区域需要重绘。
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //控件的最小宽度,如果控件宽度小于这个值,则需要自动调整控件的宽度(2*起始矩形的位置X+20个矩形单位+7个间隔)
            controlMinWidth = 2 * TimePointX + 20 * TimeRectangleUnit + 7 * TimeEveryIntervalX;
            //控件的最小高度,如果控件高度小于这个值,则需要自动调整控件的高度(2*起始矩形的位置Y+5个矩形单位)
            controlMinHeigh = 2 * TimePointY + 5 * TimeRectangleUnit;
            if (this.Width < controlMinWidth)
            {
                this.Width = controlMinWidth;
            }
            if (this.Height < controlMinHeigh)
            {
                this.Height = controlMinHeigh;
            }



            Graphics g = e.Graphics; //获取画布
            //画背景
            using (Brush brush = new SolidBrush(TimeBackGroundColor))
            {
                
                g.FillRectangles(brush, DrawTimeRectangle().Item1);
          
            }
            //画倒计时的数字
            if (DrawTimeRectangle().Item2 != null && DrawTimeRectangle().Item2.Length > 0)
            {
                using (Brush brush = new SolidBrush(TimeNumColor))
                {
                    //画倒计时的数字
                    g.FillRectangles(brush, DrawTimeRectangle().Item2);

                }
            }



        }

        /// <summary>
        /// 启用倒计时
        /// </summary>
        private void StartCountdownTimer()
        {


            if (!TimeCountDownStart) { return; }
         
            if (timer!=null)
            {
                    timer.Stop();
                    timer.Tick -= Timer_Tick; // 解除事件绑定，防止内存泄漏
                    timer.Dispose();
            }
          
                tempTime = timeCountDown;
                timer = new Timer();
                timer.Interval = 1000;//初始化timer
                timer.Tick += Timer_Tick; //注册计时器事件
                timer.Start();
            

               

            }
        

        /// <summary>
        /// 控件加载事件,倒计时开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountdownTimer_Load(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                MessageBox.Show("倒计时控件加载完成,请设置倒计时的时间,单位秒", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartCountdownTimer();
            }
            

        }
        /// <summary>
        /// 计时器倒计时事件
        /// </summary>
        /// D 或 D2, D4 等	十进制格式（带前导零）	123.ToString("D4") → "0123"
        //  N 或 N2 数字格式（千分位+保留小数）	(12345.678).ToString("N2") → "12,345.68"
        //  F 或 F2 固定点格式（强制保留小数位）	(123.456).ToString("F2") → "123.46"
        //  X 或 X4 十六进制格式	255.ToString("X2") → "FF"
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {

            tempTime--;
            if (tempTime == 0)
            {
                tempTime = timeCountDown;
            }
            //倒计时开始，并转为字符数组给绘制方法使用
            strTempTime =tempTime.ToString();
            int legth = strTempTime.Length;
            //6位不足前面补零
            //for (int i = 0; i < 6- legth; i++)
            //{
            //    strTempTime="0" + strTempTime;
            //}
            strTempTime = tempTime.ToString("D6"); // 自动补零到6位 
            charsTime = strTempTime.ToCharArray();
            Invalidate();
            
            

        }

        /// <summary>
        /// 绘制数字0的矩形
        /// </summary>
        /// <param name="startPointX">起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum0(int startPointX)
        {
            int width = 3; // 宽度为3个单位（即"0"的宽度）
            int height = 5; // 高度为5个单位（即"0"的高度）

            Rectangle[] rectangles = new Rectangle[12]; // 计算所需矩形数量

            int index = 0;
            for (int i = 0; i < width; i++)
            {
                // 上边框
                rectangles[index++] = new Rectangle(startPointX + i * timeRectangleUnit, TimePointY, timeRectangleUnit, TimeRectangleUnit);
                // timeRectangleUnit
                rectangles[index++] = new Rectangle(startPointX + i * timeRectangleUnit, TimePointY + (height - 1) * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
            }

            for (int j = 1; j < height - 1; j++)
            {
                // 左边框
                rectangles[index++] = new Rectangle(startPointX, TimePointY + j * timeRectangleUnit, timeRectangleUnit, timeRectangleUnit);
                // 右边框
                rectangles[index++] = new Rectangle(startPointX + (width - 1) * timeRectangleUnit, TimePointY + j * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            }
            return rectangles;

        }
        /// <summary>
        /// 绘制数字1的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum1(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[5];
            for (int i = 0; i < 5; i++)
            {
                rectangles[i] = new Rectangle(startPointX+2* timeRectangleUnit, TimePointY+i* timeRectangleUnit,   timeRectangleUnit, timeRectangleUnit);
            }
            return rectangles;
        }
        /// <summary>
        /// 绘制数字2的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum2(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[11];
            //从上到下先画三条横线
            //先画三条横线
            int index = DrawLineOfEight(rectangles, startPointX);
            //再补两个竖线
            rectangles[index++] = new Rectangle(startPointX + 2* TimeRectangleUnit, TimePointY+ TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//上
            rectangles[index++] = new Rectangle(startPointX, TimePointY +3*TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//下
            return rectangles;
        }
        /// <summary>
        /// 绘制数字3的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum3(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[11];
            //从上到下先画三条横线

            //先画三条横线
            int index = DrawLineOfEight(rectangles, startPointX);
            //再补两个竖线
            rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY + TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//上
            rectangles[index++] = new Rectangle(startPointX+ 2 * TimeRectangleUnit, TimePointY + 3 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//下
            return rectangles;
        }
        /// <summary>
        /// 绘制数字4的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum4(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[9];
            //从左到右先画两条竖线
        
            //先画中间一条横线
            int index = DrawLineOfEight(rectangles, startPointX,false,true,false);
            //再补上下4个矩形
            for (int i = 0; i < 2; i++)
            {
                rectangles[index++] = new Rectangle(startPointX , TimePointY + i * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//左
                rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY + i * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//右上
                rectangles[index++] = new Rectangle(startPointX+2* TimeRectangleUnit, TimePointY + (i+3) * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//右下
            }
           

            return rectangles;
        }
        /// <summary>
        /// 绘制数字5的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum5(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[11];
            //先画三条横线
            int index= DrawLineOfEight(rectangles, startPointX);
            //再补两个矩形
            rectangles[index++] = new Rectangle(startPointX, TimePointY +  TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//左
            rectangles[index++] = new Rectangle(startPointX+2* TimeRectangleUnit, TimePointY + 3*TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//右
            return rectangles;
        }
        /// <summary>
        /// 绘制数字6的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum6(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[10];
            //先画两条横线
            int index = DrawLineOfEight(rectangles, startPointX,false);
            //再补四个矩形
            rectangles[index++] = new Rectangle(startPointX, TimePointY , TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX, TimePointY + TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX, TimePointY + 3 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX+2* TimeRectangleUnit, TimePointY + 3 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);

            return rectangles;
        }
        /// <summary>
        /// 绘制数字7的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum7(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[7];
            //先画一条横线
            int index = DrawLineOfEight(rectangles, startPointX, true,false,false);
            //再补四个矩形
            for (int i = 0; i < 4; i++)
            {
                rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY+((i+1)*TimeRectangleUnit), TimeRectangleUnit, TimeRectangleUnit);//上
            }

            return rectangles;
        }
        /// <summary>
        /// 绘制数字8的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum8(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[13];
            //先画三条横线
            int index = DrawLineOfEight(rectangles, startPointX);
            //再补四个矩形
            for (int i = 0; i < 2; i++)
            {
                rectangles[index++] = new Rectangle(startPointX , TimePointY + (2*i+1)*TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//左
                rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY + (2 * i + 1) * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//右
            }

            return rectangles;
        }
        /// <summary>
        /// 绘制数字9的矩形
        /// </summary>
        /// <param name="startPointX">8字左上角起始的X坐标</param>
        /// <returns>所有的矩形数组</returns>
        private Rectangle[] DrawNum9(int startPointX)
        {

            Rectangle[] rectangles = new Rectangle[10];
            //先画三条横线
            int index = DrawLineOfEight(rectangles, startPointX,true,true,false);
            //再补四个矩形
            rectangles[index++] = new Rectangle(startPointX , TimePointY + 1 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY +1 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY + 3 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            rectangles[index++] = new Rectangle(startPointX + 2 * TimeRectangleUnit, TimePointY + 4 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);
            return rectangles;
        }
        /// <summary>i
        /// 画8的上，中，下三条横线
        /// </summary>
        /// <param name="top">上</param>
        /// <param name="center">中</param>
        /// <param name="bottom">下</param>
        private int DrawLineOfEight(Rectangle[] rectangles,int startPointX, bool top =true,bool center=true,bool bottom=true)
        {
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                if (top)
                {
                    rectangles[index++] = new Rectangle(startPointX + i * TimeRectangleUnit, TimePointY, TimeRectangleUnit, TimeRectangleUnit);//上

                }
                if (center)
                {
                    rectangles[index++] = new Rectangle(startPointX + i * TimeRectangleUnit, TimePointY + 2 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//中
                }
                if (bottom)
                {
                    rectangles[index++] = new Rectangle(startPointX + i * TimeRectangleUnit, TimePointY + 4 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);//下
                }
                
            }
            return index;


        }

        /// <summary>
        /// 绘制冒号
        /// </summary>
        /// <param name="startPointX"></param>
        /// <returns></returns>
        private Rectangle[] DrawColon(int startPointX)
        {
            Rectangle[] rectangles = new Rectangle[2];

            rectangles[0] = new Rectangle(startPointX, TimePointY + TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);

            rectangles[1] = new Rectangle(startPointX, TimePointY + 3 * TimeRectangleUnit, TimeRectangleUnit, TimeRectangleUnit);

            return rectangles;
        }
    }
}
