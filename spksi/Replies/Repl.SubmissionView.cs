using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace spi.Replies;
public abstract partial class Repl
{
    private sealed class SubmissionView
    {
        private readonly LineRenderHandler _lineRenderer;
        private readonly ObservableCollection<string> _submissionDocument;
        private int _cursorTop;
        private int _renderedLineCount;
        private int _currentLine;
        private int _currentCharacter;

        public SubmissionView(LineRenderHandler lineRenderer, ObservableCollection<string> submissionDocument)
        {
            _lineRenderer = lineRenderer;
            _submissionDocument = submissionDocument;
            _submissionDocument.CollectionChanged += SubmissionDocumentChanged;
            _cursorTop = Console.CursorTop;
            Render();
        }

        private void SubmissionDocumentChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            Console.CursorVisible = false;
            var lineCount = 0;
            var state = (object)null;
            foreach (var line in _submissionDocument)
            {
                Console.SetCursorPosition(0, _cursorTop + lineCount);
                Console.ForegroundColor = ConsoleColor.Green;
                if (lineCount == 0)
                {
                    Console.Write("» ");
                }
                else
                {
                    Console.Write("· ");
                }
                Console.ResetColor();
                //Console.WriteLine(DateTime.UtcNow);
                _lineRenderer(_submissionDocument, lineCount, state);
                //Console.WriteLine("jumped");
                Console.WriteLine(new string(' ', Console.WindowWidth - line.Length - 2));
                lineCount++;
            }

            var numberOfBlankLines = _renderedLineCount - lineCount;

            if (numberOfBlankLines > 0)
            {
                var blankLine = new string(' ', Console.WindowWidth);
                for (var i = 0; i < numberOfBlankLines; i++)
                {
                    Console.SetCursorPosition(0, _cursorTop + lineCount + i);
                    Console.WriteLine(blankLine);
                }
            }

            _renderedLineCount = lineCount;
            Console.CursorVisible = true;
            UpdateCursorPosition();

        }

        private void UpdateCursorPosition()
        {
            Console.CursorTop = _cursorTop + CurrentLine;
            Console.CursorLeft = 2 + CurrentCharacter;
        }

        public int CurrentLine
        {
            get => _currentLine;
            set
            {
                if (_currentLine != value)
                {
                    _currentLine = value;
                    _currentCharacter = Math.Min(_submissionDocument[_currentLine].Length, CurrentCharacter);
                    UpdateCursorPosition();
                }
            }
        }
        public int CurrentCharacter
        {
            get => _currentCharacter;
            set
            {
                if (_currentCharacter != value)
                {
                    _currentCharacter = value;
                    UpdateCursorPosition();
                }
            }
        }

    }
}