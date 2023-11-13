int n = Convert.ToInt32(Console.ReadLine().Trim());

string password = Console.ReadLine();

int answer = minimumNumber(n,password);

Console.WriteLine(answer);

static int minimumNumber(int n, string password)
{
	var builder = new PasswordRequirementBuilder(password)
		.WithMinimumLength(6)
		.RequiresDigit()
		.RequiresLowercase()
		.RequiresUppercase()
		.RequiresSpecialCharacter();

	return builder.Build();
}

public class PasswordRequirementBuilder
{
	private readonly string _password;
	private int _minLength;
	private int _requiredTypesMissing;

	public PasswordRequirementBuilder(string password)
	{
		_password = password;
	}

	public PasswordRequirementBuilder WithMinimumLength(int minLength)
	{
		_minLength = minLength;
		return this;
	}

	public PasswordRequirementBuilder RequiresDigit()
	{
		if (!_password.Any(char.IsDigit))
			_requiredTypesMissing++;
		return this;
	}

	public PasswordRequirementBuilder RequiresLowercase()
	{
		if (!_password.Any(char.IsLower))
			_requiredTypesMissing++;
		return this;
	}

	public PasswordRequirementBuilder RequiresUppercase()
	{
		if (!_password.Any(char.IsUpper))
			_requiredTypesMissing++;
		return this;
	}

	public PasswordRequirementBuilder RequiresSpecialCharacter()
	{
		if (!_password.Any(c => char.IsPunctuation(c) || char.IsSymbol(c)))
			_requiredTypesMissing++;
		return this;
	}

	public int Build()
	{
		return Math.Max(_requiredTypesMissing, _minLength - _password.Length);
	}
}