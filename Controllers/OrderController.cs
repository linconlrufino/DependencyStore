using Dapper;
using DependencyStore.Models;
using DependencyStore.Repositories.Contracts;
using DependencyStore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Controllers;

public class OrderController : ControllerBase
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeService _deliveryFeeService;
    private readonly IPromoCodeRepository _promoCoderepository;

    public OrderController(
        ICustomerRepository customerRepository,
        IDeliveryFeeService deliveryFeeService,
        IPromoCodeRepository promoCoderepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeService = deliveryFeeService;
        _promoCoderepository = promoCoderepository;
    }

    [Route("v1/orders")]
    [HttpPost]
    public async Task<IActionResult> Place(string customerId, string zipCode, string promoCode, int[] products)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer == null)
            return NotFound();

        decimal deliveryFee = await _deliveryFeeService.GetDeliveryFeeAsync(zipCode);
        var cupon = await _promoCoderepository.GetPromoCodeAsync(promoCode);
        var discount = cupon?.Value ?? 0M;

        var order = new Order(deliveryFee, discount, new List<Product>());

        return Ok($"Pedido {order.Code} gerado com sucesso!");
    }
}