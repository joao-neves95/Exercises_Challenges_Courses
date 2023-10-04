variable "az_subscription_id" {
  type    = string
  sensitive = true
}

variable "az_tenant_id" {
  type    = string
  sensitive = true
}

variable "az_client_id" {
  type    = string
  sensitive = true
}

variable "az_client_secret" {
  type    = string
  sensitive = true
}

variable "az_resource_group_name" {
  type    = string
  default = "eShop"
}

variable "az_location" {
  type    = string
  default = "eastus"
}

variable "az_domain_name" {
  type    = string
  default = "eshop-webapp"
}

variable "service_docker_image" {
  type    = string
  default = "joaoneves95/AspnetRunBasics"
}

variable "service_docker_image_tag" {
  type    = string
  default = "latest"
}

variable "service_env_ASPNETCORE_ENVIRONMENT" {
  type    = string
  default = "Production"
}

variable "service_env_ApiSettings__GatewayAddress" {
  type    = string
  default = "http://eshop-apigateways-web"
}
