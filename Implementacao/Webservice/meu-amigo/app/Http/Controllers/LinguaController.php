<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Lingua;

class LinguaController extends Controller
{
    public function index()
    {
        return Lingua::all();
    }
}
