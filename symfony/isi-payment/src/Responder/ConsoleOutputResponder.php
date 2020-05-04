<?php
/**
 * @category    symfony
 * @date        02/05/2020
 * @author      Michał Bolka <mbolka@divante.co>
 * @copyright   Copyright (c) 2020 Divante Ltd. (https://divante.co)
 */

namespace App\Responder;

use App\Common\Result;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Input\InputOption;
use Symfony\Component\Console\Output\Output;
use Symfony\Component\Console\Output\OutputInterface;
use Symfony\Component\Console\Style\SymfonyStyle;

class ConsoleOutputResponder
{
    private SymfonyStyle $output;
    public function configure(InputInterface $input, OutputInterface $output)
    {
        $this->output = new SymfonyStyle($input, $output);
    }

    public function respond(Result $result)
    {
        if (!$this->output instanceof SymfonyStyle) {
            throw new \InvalidArgumentException("Responder was not configured!");
        }
        if ($result->isSuccessful()) {
            $this->output->success((string) $result);
        } else {
            $this->output->error((string) $result);
        }
    }

}