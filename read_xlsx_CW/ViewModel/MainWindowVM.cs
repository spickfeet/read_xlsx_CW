using CW_read_xlsx.State;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CW_read_xlsx.Files;
using CW_read_xlsx.State;
using CW_read_xlsx.Tokens;
using System.Windows.Controls;

namespace CW_read_xlsx.ViewModel
{
    class MainWindowVM : BasicVM
    {
        private FileWorker _fileWorker;
        private string _textInput;
        private string _debugText;

        private bool _fileSaved;
        public Action TextChanged;
        List<TokenData> _tokens;
        public string TextInput
        {
            get { return _textInput; }
            set 
            {
                Set(ref _textInput, value);
                _fileSaved = false;
                TextChanged?.Invoke();
            }
        }
        
        public string DebugText
        {
            get { return _debugText; }
            set
            {
                Set(ref _debugText, value);
            }
        }
        public List<TokenData> Tokens
        {
            get { return _tokens; }
            set
            {
                Set(ref _tokens, value);
            }
        }

        public Action<string> PathChanged;
        public MainWindowVM()
        {
            _textInput = "";
            _fileSaved = true;
            _fileWorker = new();
            PathChanged += _fileWorker.OnPathChanged;
        }
        private bool TryChoosePath()
        {
            SaveFileDialog fileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string pathName = fileDialog.FileName;
                PathChanged?.Invoke(pathName);

                if (!File.Exists(pathName))
                    File.Create(pathName).Close();

                return true;
            }
            else { return false; }
        }

        private bool TryOpen()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string pathName = fileDialog.FileName;
                PathChanged?.Invoke(pathName);

                return true;
            }
            else { return false; }
        }

        public void PromptSave() // Изменить название
        {
            while (_fileSaved == false)
            {
                MessageBoxResult result = MessageBox.Show(
                "У вас есть несохраненные изменения. Хотите сохранить файл перед выходом?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Save();
                }
                else if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
        }

        private void Open()
        {
            PromptSave();
            if (TryOpen() == false) return;
            TextInput = _fileWorker.GetData();
            _fileSaved = true;

        }

        private void Create()
        {
            PromptSave();
             TextInput = "";
            _fileWorker.OnPathChanged(null);
            _fileSaved = true;

        }

        private void Save()
        {
            if (string.IsNullOrEmpty(_fileWorker.PathName))
                if (TryChoosePath() == false) return;
            _fileWorker.Save(TextInput);
            _fileSaved = true;
        }

        private void SaveAs()
        {
            if (TryChoosePath() == false) return;
            _fileWorker.Save(TextInput);
            _fileSaved = true;
        }

        public ICommand CreateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Create();
                });
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Open();
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Save();
                });
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SaveAs();
                });
            }
        }
        public ICommand Start
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    List<ErrorData> errorsData = new();
                    Tokens = TokenConverter.CreateTokens(TextInput, errorsData);
                    StateMachine stateMachine = new(Tokens, errorsData);
                    stateMachine.Start();
                    DebugText = "";
                    if (stateMachine.ErrorsData.Count == 0)
                    {
                        DebugText = "Ошибок нет";
                    }
                    else
                    {
                        errorsData = errorsData.OrderBy(b => b.GlobalIndex).ToList();
                        DebugText = $"Строка: {errorsData[0].Line} Глобальный индекс: {errorsData[0].LocalIndex} Ошибка {errorsData[0].Text} \n";
                    }
                });
            }
        }
    }
}
