#TODO: authenticate - https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/guides/service_principal_client_certificate
provider "azurerm" {
  features {}

  # https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/guides/service_principal_client_secret#creating-a-service-principal-in-the-azure-portal
  subscription_id = var.az_subscription_id
  tenant_id       = var.az_tenant_id
  client_id       = var.az_client_id
  client_secret   = var.az_client_secret
}

resource "azurerm_resource_group" "appresourcegroup" {
  name     = var.az_resource_group_name
  location = var.az_location
}

resource "azurerm_service_plan" "appserviceplan" {
  name                = "${var.az_domain_name}-asp"
  resource_group_name = var.az_resource_group_name
  location            = var.az_location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "webapp" {
  name                = var.az_domain_name
  resource_group_name = var.az_resource_group_name
  service_plan_id     = azurerm_service_plan.appserviceplan.id
  location            = var.az_location

  site_config {
    application_stack {
      docker_image_name   = "${var.service_docker_image}:${var.service_docker_image_tag}"
      docker_registry_url = "https://docker.io"
    }
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT = var.service_env_ASPNETCORE_ENVIRONMENT
  }
}
