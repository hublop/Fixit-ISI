<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Action\Command;

use App\Application\RecurringPayment\RecurringPaymentManager;
use App\Responder\ConsoleOutputResponder;
use Symfony\Component\Console\Command\Command;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Output\OutputInterface;

class RecurringPaymentCommand extends Command
{
    protected static $defaultName = "payment:recurring:run";
    /**
     * @var RecurringPaymentManager
     */
    private RecurringPaymentManager $domain;
    /**
     * @var ConsoleOutputResponder
     */
    private ConsoleOutputResponder $responder;

    public function __construct(RecurringPaymentManager $domain, ConsoleOutputResponder $responder, string $name = null)
    {
        $this->domain = $domain;
        $this->responder = $responder;
        parent::__construct($name);
    }

    public function execute(InputInterface $input, OutputInterface $output)
    {
        $this->responder->configure($input, $output);
        $time = new \DateTimeImmutable();

        foreach ($this->domain->processPayments($time) as $result) {
            $this->responder->respond($result);
        }
        return 0;
    }
}