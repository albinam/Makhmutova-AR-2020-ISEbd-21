using DinerBusinessLogic.Attributes;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DinerClientView
{
    static class Program
    {
        public static ClientViewModel Client { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APIClient.Connect();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new FormEnter();
            form.ShowDialog();
            if (Client != null)
            {
                Application.Run(new FormMain());
            }
        }
        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            if (type.BaseType == typeof(BaseViewModel))
            {
                // создаем объект от типа
                object obj = Activator.CreateInstance(type);
                // вытаскиваем метод получения списка заголовков
                var method = type.GetMethod("Properties");
                // вызываем метод
                var config = (List<string>)method.Invoke(obj, null);
                grid.Columns.Clear();
                foreach (var conf in config)
                {
                    // вытаскиваем нужное свойство из класса
                    var prop = type.GetProperty(conf);
                    if (prop != null)
                    {
                        // получаем список атрибутов
                        var attributes =
                        prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                        if (attributes != null && attributes.Length > 0)
                        {
                            foreach (var attr in attributes)
                            {
                                // ищем нужный нам атрибут
                                if (attr is ColumnAttribute columnAttr)
                                {
                                    var column = new DataGridViewTextBoxColumn
                                    {
                                        Name = conf,
                                        ReadOnly = true,
                                        HeaderText = columnAttr.Title,
                                        Visible = columnAttr.Visible,
                                        Width = columnAttr.Width
                                    };
                                    if (columnAttr.GridViewAutoSize !=
                                    GridViewAutoSize.None)
                                    {
                                        column.AutoSizeMode =
                                       (DataGridViewAutoSizeColumnMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnMode),
                                       columnAttr.GridViewAutoSize.ToString());
                                    }
                                    grid.Columns.Add(column);
                                }
                            }
                        }
                    }
                }
                // добавляем строки
                foreach (var elem in data)
                {
                    List<object> objs = new List<object>();
                    foreach (var conf in config)
                    {
                        var value = elem.GetType().GetProperty(conf).GetValue(elem);
                        objs.Add(value);
                    }
                    grid.Rows.Add(objs.ToArray());
                }
            }
        }
    }
}
