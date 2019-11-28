namespace teste_delegate
{
    delegate int CalculoEntreDoisNumeros(int numero1, int numero2);
    delegate T CalculoEntreDoisNumerosGenerico<T>(T numero1, T numero2);

    delegate bool CondicaoPost(Post post);
    delegate bool CondicaoPost<T>(T post);
    delegate void AcaoEmUmPost(Post post);
}