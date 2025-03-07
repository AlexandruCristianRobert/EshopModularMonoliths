string input = "life is awesome";
string result = string.Empty;

List<int> indexes = new List<int>();
int endIndex = input.Length - 1;

//for(int i = 0; i <= input.Length - 1; i++)
//{
//    if (input[i] == ' ')
//    {
//        result += ' ';
//        continue;
//    }

//    result += input[endIndex] >= 'a' && input[endIndex] <= 'z' ? input[endIndex] : string.Empty;
//    endIndex--;
//}
int startIndex = 0;

while(endIndex >= 0)
{
    if (startIndex <= input.Length - 1 &&  input[startIndex] == ' ')
    {
        result += ' ';
        startIndex++;
        continue;
    }
    result += input[endIndex] >= 'a' && input[endIndex] <= 'z' ? input[endIndex] : string.Empty;
    startIndex++;
    endIndex--;
}




//for (int i = 0; i < input.Length; i++)
//    if (input[i] == ' ')
//        indexes.Add(i);

//for (int i = input.Length - 1; i >= 0; i--)
//{
//    if (indexes.Contains(result.Length))
//    {
//        result += ' ';
//    }

//    if (input[i] >= 'a' && input[i] <= 'z')
//    {
//        result += input[i];
//    }
//}




Console.WriteLine(result);