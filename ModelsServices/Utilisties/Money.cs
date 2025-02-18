namespace ModelsServices.Utilities
{
    public class Money
    {
        decimal _value;
        Monnaie _monnaie;
        public Money(int montant, Monnaie monnaie)
        {
            _value = montant;
            _monnaie = monnaie;
        }

        public Money(decimal montant, Monnaie monnaie)
        {
            _value = montant;
            _monnaie = monnaie;
        }

        public Money(int montant, int monnaie)
        {
            _value = montant;
            _monnaie = (Monnaie)monnaie;
        }

        public Money(float montant, int monnaie)
        {
            _value = (decimal)montant;
            _monnaie = (Monnaie)monnaie;
        }

        public Money(decimal montant, int monnaie)
        {
            _value = montant;
            _monnaie = (Monnaie)monnaie;
        }
        public Money(float montant, Monnaie monnaie)
        {
            _value = (decimal)montant;
            _monnaie = monnaie;
        }

        public override string ToString()
        {
            return _value + " " + _monnaie.ToString();
        }

        public int GetIntMonnaie()
        {
            return (int)_monnaie;
        }

        public Monnaie GetMonnaie()
        {
            return _monnaie;
        }

        public decimal GetValue()
        {
            return _value;
        }
    }
}
