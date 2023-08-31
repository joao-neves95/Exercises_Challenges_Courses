# Terraform
---

## Introduction
- Allows infrastructure to be expressed as code, HCL (Terraform Hashicorp Configuration Language).
- Terraform uses HCL to provide an execution plan of changes.
- Allows to manage a broad range of resources, including hardware, IaaS, PaaS, and SaaS services.

---

## Infrastructure as Code (IaC)
- Code that defines and displays infrastructure resources onto various platforms, instead of managing them manually.
- To achieve a consistent and predictable environment.
- Why to use:
    - Automation.
    - Source controllable.
    - Declarative and consistent infrastructure.
    - Speed, cost and lower risk of human error.
    - Idempotent (automatically tracks the state of resources deployed and to be deployed; maintains the state of the already created resources).
    - Private and public cloud vendor independent.
- Types of IaC:
    - Configuration management:
        - E.g.: Ansible, Puppet, etc.
        - Install and manage software.
        - Version control.
        - Idempotent.
    - Server templating:
        - E.g.: Docker, Vagrant, etc.
        - Pre-installed software and dependencies.
        - Containers or VMs.
        - Immutable.
    - Provisioning tools:
        - E.g.: Terraform, CloudFormation, etc.
        - Install and manage software.
        - Version control.
        - Idempotent.
- [More](learning-notes/dev/microsoft-certs/az-900/1.1_describe-core-azure-concepts#types-of-cloud-services-models)

---

## Terraform Execution Flow
1. Init:
    - `terraform init`
    - Initialise a new or existing working Terraform directory along with all the config files and dependencies.
3. Plan:
    - `terraform plan`
    - Creates an execution plan.
    - It idempotently scans the config files to identify what actions need to be performed in order to achieve the desired state.
    - It also enables a preview of the changes that will be done, before doing any change to the real infrastructure.
5. Apply:
    - `terraform apply`
    - It performs the actions needed to be performed in order to achieve the desired state specified in the config files.

---

## Terraform Files
- `terraform.tfstate`
    - The file that stores the current state of the infrastructure, along with all the IDs of the instances and other metadata.
    - Never manually edit this file. It is auto-generated.
- `terraform.lock.hcl`
    - Locks all the preovider versions.
- `.terraform/` (directory/folder)
    - It stores all providers.

---

## CLI Commands

### Print version and check if a Terraform instance is running.
```bash
terraform --version
```

### Init
```bash
terraform init
```

### Create an execution plan
```bash
terraform plan
```

### Apply the plan
```bash
terraform apply
```

### Destroy all resources
```bash
terraform destroy
```

### State
```bash
# List all resources in state.
terraform state list

# Print the details of a specific resource.
terraform state show {resource-name}
```

---

## HCL
- Hashicorp Configuration Language.
- The domain specific language used to write the IaC on Terraform.
- File extension: `.tf`.
- https://developer.hashicorp.com/terraform/language

### Basic structure of a resource

```terraform
resource "{resource type}" "some_name" {
    // Arguments.
    // (E.g.:)
    filename = "/root/games.txt"
    content = "foobar"
}
```

### Input Variables
- Like function arguments.
- Reference the variable by referencing `var.{variable_name}` inside another resource.
- Example:
```terraform
variable "{variable_name}" {
    default = "some defualt value"

    type = string // Optional. Defualt is "any". There's "string", "number", "bool", "list", "map", etc.
    description = some descript // Optional.
}
```
- It is also possible to use a `terraform.tfvars` or a `terraform.tfvars.json` file to manage variables.

### Output Variables
- Returns values from a Terraform module.

### Resources
- The main purpose of the Terraform language is to declare resources.
- All other language features exist only to make the definition of resources more flexible and convenient.
- They represent infrastructure objects.

### Providers
- Plugins to interact with external providers (e.g. cloud providers, SaaS providers, other APIs).
- TF configs must declare which providers they require so that Terraform can install and use them.
- Each provider adds a set of **resource** types and/or **data sources** that Terraform can manage.

### Modules
- A wrapper around a collection of Terraform resources.
- Self-contained packages of TF configurations that are managed as a group.

---
