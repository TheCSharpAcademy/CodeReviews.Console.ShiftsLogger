public static class WorkerFilterOptionsDefaults
{
	public static WorkerFilterOptions GetDefault( )
	{
		return new WorkerFilterOptions
		{
			WorkerId = null ,
			Name = string.Empty ,
			Phone = string.Empty ,
			Email = string.Empty ,
			SortBy = "Name" ,
			SortOrder = "asc" ,
			Search = string.Empty
		};
	}
}
