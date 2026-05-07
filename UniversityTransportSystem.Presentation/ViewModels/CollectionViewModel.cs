using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversityTransportSystem.Business.Services;

namespace UniversityTransportSystem.Presentation.ViewModels;

public class CollectionViewModel<T> : BaseViewModel where T : class
{
    private readonly IService<T> _service;
    
    public ObservableCollection<T> Items { get; } = new();
    
    private T? _selectedItem;
    public T? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            _ = LoadAsync();
        }
    }

    public CollectionViewModel(IService<T> service)
    {
        _service = service;
    }

    public async Task LoadAsync()
    {
        IsLoading = true;
        try
        {
            var items = await _service.GetAllAsync();
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (result)
            await LoadAsync();
        return result;
    }
}
